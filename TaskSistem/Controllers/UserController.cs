using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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
            return Ok(users);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<UserModel>> FindOne([FromRoute] int id)
        {
            UserModel user = await _userRepository.FindOne(id);
            return Ok(user);
        }

        [HttpPost]
        public async Task<ActionResult<UserModel>> Insert([FromBody] UserModel data)
        {
            UserModel user = await _userRepository.Insert(data);
            return Ok(user);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<UserModel>> Update([FromBody] UserModel data, [FromRoute] int id)
        {
            UserModel user = await _userRepository.Update(data, id);
            return Ok(user);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<bool>> Delete([FromRoute] int id)
        {
            bool isDeleted = await _userRepository.Delete(id);
            return Ok(isDeleted);
        }
    }
}
