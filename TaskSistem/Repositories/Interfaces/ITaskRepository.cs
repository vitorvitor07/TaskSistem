using TaskSistem.Models;

namespace TaskSistem.Repositories.Interfaces
{
    public interface ITaskRepository
    {
        Task<List<TaskModel>> FindAll();
        Task<List<TaskModel>> FindAllByUserId(int Id);
        Task<TaskModel> FindOne(int Id);
        Task<TaskModel> Insert(TaskModel User);
        Task<TaskModel> Update(TaskModel User, int Id);
        Task<bool> Delete(int Id);

    }
}
