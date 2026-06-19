using Mapster;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TaskSistem.Dtos.User;
using TaskSistem.Models;
using TaskSistem.Services.Interfaces;

namespace TaskSistem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        public async Task<ActionResult<List<UserToResponseDto>>> FindAll()
        {
            return Ok(await _userService.ListUsers());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<UserModel>> FindOne([FromRoute] int id)
        {
            return Ok(await _userService.FindOne(id));
        }

        [HttpPost]
        public async Task<ActionResult<UserToResponseDto>> Insert([FromBody] InsertUserDto data)
        {
            return Ok(await _userService.Insert(data));
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<UserModel>> Update([FromBody] UpdateUserDto data, [FromRoute] int id)
        {
            return Ok(await _userService.Update(id, data));
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<bool>> Delete([FromRoute] int id)
        {
            bool isDeleted = await _userService.Delete(id);
            return Ok(new { isDeleted });
        }
    }
}
