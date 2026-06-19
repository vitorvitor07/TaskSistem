using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TaskSistem.Models;
using TaskSistem.Repositories.Interfaces;

namespace TaskSistem.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class TaskController : ControllerBase
    {
        private readonly ITaskRepository _taskRepository;

        public TaskController(ITaskRepository TaskRepository)
        {
            _taskRepository = TaskRepository;
        }

        [HttpGet]
        public async Task<ActionResult<List<TaskModel>>> FindAll()
        {
            List<TaskModel> Task = await _taskRepository.FindAll();
            return Ok(Task);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<TaskModel>> FindOne([FromRoute] int id)
        {
            TaskModel Task = await _taskRepository.FindOne(id);
            return Ok(Task);
        }

        [HttpPost]
        public async Task<ActionResult<TaskModel>> Insert([FromBody] TaskModel data)
        {
            TaskModel Task = await _taskRepository.Insert(data);
            return Ok(Task);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<TaskModel>> Update([FromBody] TaskModel data, [FromRoute] int id)
        {
            data.Id = id;
            TaskModel Task = await _taskRepository.Update(data, id);
            return Ok(Task);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<bool>> Delete([FromRoute] int id)
        {
            bool isDeleted = await _taskRepository.Delete(id);
            return Ok(isDeleted);
        }
    }
}
