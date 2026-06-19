
using TaskSistem.Dtos.Task;

namespace TaskSistem.Services.Interfaces
{
    public interface ITaskService
    {
        public Task<List<TaskToResponseDto>> ListTasks(int userId);
        public Task<TaskToResponseDto> FindOne(int id, int userId);
        public Task<TaskToResponseDto> Insert(InsertTaskDto data, int userId);
        public Task<TaskToResponseDto> Update(int id, UpdateTaskDto data, int userId);
        public Task<bool> Delete(int id, int userId);
    }
}
