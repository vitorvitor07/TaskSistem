using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TaskSistem.Dtos.Task;
using TaskSistem.Services.Interfaces;

namespace TaskSistem.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class TaskController : ControllerBase
    {
        private readonly ITaskService _taskService;

        public TaskController(ITaskService taskService)
        {
            _taskService = taskService;
        }

        [HttpGet]
        public async Task<ActionResult<List<TaskToResponseDto>>> FindAll()
        {
            int userId = GetUserId();
            var tasks = await _taskService.ListTasks(userId);
            return Ok(tasks);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<TaskToResponseDto>> FindOne([FromRoute] int id)
        {
            int userId = GetUserId();
            var task = await _taskService.FindOne(id, userId);
            return Ok(task);
        }

        [HttpPost]
        public async Task<ActionResult<TaskToResponseDto>> Insert([FromBody] InsertTaskDto data)
        {
            int userId = GetUserId();
            var createdTask = await _taskService.Insert(data, userId);
            return Ok(createdTask);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<TaskToResponseDto>> Update([FromRoute] int id, [FromBody] UpdateTaskDto data)
        {
            int userId = GetUserId();
            var updatedTask = await _taskService.Update(id, data, userId);
            return Ok(updatedTask);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<bool>> Delete([FromRoute] int id)
        {
            int userId = GetUserId();
            bool isDeleted = await _taskService.Delete(id, userId);
            return Ok(new { isDeleted });
        }

        private int GetUserId()
        {
            var claimValue = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;

            if (string.IsNullOrEmpty(claimValue))
            {
                throw new UnauthorizedAccessException("Usuário não identificado no Token.");
            }

            return int.Parse(claimValue);
        }
    }
}