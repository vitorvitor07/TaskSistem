using Mapster;
using TaskSistem.Dtos.Task;
using TaskSistem.Models;
using TaskSistem.Repositories.Interfaces;
using TaskSistem.Services.Interfaces;

namespace TaskSistem.Services
{
    public class TaskService : ITaskService
    {
        private readonly ITaskRepository _taskRepository;

        public TaskService(ITaskRepository taskRepository)
        {
            _taskRepository = taskRepository;
        }

        public async Task<List<TaskToResponseDto>> ListTasks(int userId)
        {
            var userTasks = await _taskRepository.FindAllByUserId(userId);
            return userTasks.Adapt<List<TaskToResponseDto>>();
        }

        public async Task<TaskToResponseDto> FindOne(int id, int userId)
        {
            TaskModel task = await GetTaskAndValidateOwnership(id, userId);
            return task.Adapt<TaskToResponseDto>();
        }

        public async Task<TaskToResponseDto> Insert(InsertTaskDto data, int userId)
        {
            var taskModel = data.Adapt<TaskModel>();

            taskModel.UserId = userId;

            TaskModel createdTask = await _taskRepository.Insert(taskModel);
            return createdTask.Adapt<TaskToResponseDto>();
        }

        public async Task<TaskToResponseDto> Update(int id, UpdateTaskDto data, int userId)
        {
            TaskModel existingTask = await GetTaskAndValidateOwnership(id, userId);
            if (data.Name != null) existingTask.Name = data.Name;
            if (data.Description != null) existingTask.Description = data.Description;
            if (data.Status.HasValue) existingTask.Status = data.Status.Value;
            TaskModel updatedTask = await _taskRepository.Update(existingTask, id);
            return updatedTask.Adapt<TaskToResponseDto>();
        }

        public async Task<bool> Delete(int id, int userId)
        {
            await GetTaskAndValidateOwnership(id, userId);
            return await _taskRepository.Delete(id);
        }
        private async Task<TaskModel> GetTaskAndValidateOwnership(int id, int userId)
        {
            TaskModel task = await _taskRepository.FindOne(id);
            if (task.UserId != userId)
            {
                throw new UnauthorizedAccessException("Access Danied");
            }
            return task;
        }
    }
}