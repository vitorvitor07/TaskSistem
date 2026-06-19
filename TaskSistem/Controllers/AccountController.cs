using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using TaskSistem.Models;
using TaskSistem.Repositories.Interfaces;

namespace TaskSistem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IUserRepository _userRepository;
        private readonly IConfiguration _configuration;


        public AccountController(IUserRepository userRepository, IConfiguration configuration)
        {
            _userRepository = userRepository;
            _configuration = configuration;
        }

        [HttpPost]
        public async Task<IActionResult> Login([FromBody] LoginModel login)
        {
            if (login.Email == null || login.Password == null) throw new Exception("email and password is required");

            UserModel user = await _userRepository.FindByEmail(login.Email);

            if (user.Password == login.Password && user.Email == login.Email)
            {
                return Ok(new { accessToken = GenerateJWT($"{user.Id}") });
            }
            else
            {
                return BadRequest(new { message = "Invalid credentials" });
            }

        }

        private string GenerateJWT(string sub)
        {
            string secret = _configuration["JwtSettings:Secret"]
                ?? throw new Exception("JWT Secret não configurada.");

            var chave = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secret));
            var credential = new SigningCredentials(chave, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim("sub", sub),
            };

            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.Now.AddDays(1),
                signingCredentials: credential,
                issuer: "devv",
                audience: "devv"
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
