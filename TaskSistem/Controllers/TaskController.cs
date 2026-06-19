using Mapster;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TaskSistem.Dtos.User;
using TaskSistem.DTOs.Task;
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
            List<TaskModel> task = await _taskRepository.FindAll();
            return Ok(task.Adapt<List<TaskToResponseDTO>>());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<TaskModel>> FindOne([FromRoute] int id)
        {
            TaskModel task = await _taskRepository.FindOne(id);
            return Ok(task.Adapt<TaskToResponseDTO>());
        }

        [HttpPost]
        public async Task<ActionResult<TaskModel>> Insert([FromBody] TaskModel data)
        {
            TaskModel task = await _taskRepository.Insert(data);
            return Ok(task.Adapt<TaskToResponseDTO>());
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<TaskModel>> Update([FromBody] TaskModel data, [FromRoute] int id)
        {
            data.Id = id;
            TaskModel task = await _taskRepository.Update(data, id);
            return Ok(task.Adapt<TaskToResponseDTO>());
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<bool>> Delete([FromRoute] int id)
        {
            bool isDeleted = await _taskRepository.Delete(id);
            return Ok(new { isDeleted });
        }
    }
}
