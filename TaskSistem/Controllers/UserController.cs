using Mapster;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TaskSistem.Dtos.User;
using TaskSistem.Models;
using TaskSistem.Repositories.Interfaces;

namespace TaskSistem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository _userRepository;

        public UserController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        [HttpGet]
        public async Task<ActionResult<List<UserModel>>> FindAll()
        {
            List<UserModel> users = await _userRepository.FindAll();
            return Ok(users.Adapt<List<UserToResponseDto>>());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<UserModel>> FindOne([FromRoute] int id)
        {
            UserModel user = await _userRepository.FindOne(id);
            return Ok(user.Adapt<UserToResponseDto>());
        }

        [HttpPost]
        public async Task<ActionResult<UserModel>> Insert([FromBody] UserModel data)
        {
            if (data.Email == null || data.Password == null)
                return BadRequest(new { message = "Email and Password is required" });

            bool userExists = await _userRepository.EmailExists(data.Email ?? "");
            if (userExists) return BadRequest("Invalid email");
            data.Password = BCrypt.Net.BCrypt.HashPassword(data.Password);
            UserModel user = await _userRepository.Insert(data);
            return Ok(user.Adapt<UserToResponseDto>());
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<UserModel>> Update([FromBody] UserModel data, [FromRoute] int id)
        {
            UserModel user = await _userRepository.Update(data, id);
            return Ok(user.Adapt<UserToResponseDto>());
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<bool>> Delete([FromRoute] int id)
        {
            bool isDeleted = await _userRepository.Delete(id);
            return Ok(new { isDeleted });
        }
    }
}
