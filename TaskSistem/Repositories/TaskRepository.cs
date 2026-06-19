using Microsoft.EntityFrameworkCore;
using TaskSistem.Data;
using TaskSistem.Models;
using TaskSistem.Repositories.Interfaces;

namespace TaskSistem.Repositories
{
    public class TaskRepository : ITaskRepository
    {
        private readonly TaskSistemDBContext _dbContext;

        public TaskRepository(TaskSistemDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<TaskModel>> FindAll()
        {
            return await _dbContext.Tasks.Include(x => x.User).ToListAsync();
        }

        public async Task<TaskModel> FindOne(int id)
        {
            TaskModel model = await _dbContext.Tasks
            .Include(x => x.User)
            .FirstOrDefaultAsync(x => x.Id == id) ?? throw new KeyNotFoundException($"Task with id {id} not found");
            return model;
        }

        public async Task<TaskModel> Insert(TaskModel task)
        {
            await _dbContext.Tasks.AddAsync(task);
            await _dbContext.SaveChangesAsync();

            return task;
        }

        public async Task<TaskModel> Update(TaskModel task, int id)
        {
            TaskModel model = await FindOne(id);

            model.Name = task.Name;
            model.Description = task.Description;
            model.Status = task.Status;
            model.UserId = task.UserId;

            _dbContext.Tasks.Update(model);
            await _dbContext.SaveChangesAsync();

            model.User = task.User;

            return model;
        }

        public async Task<bool> Delete(int id)
        {
            TaskModel model = await FindOne(id);

            _dbContext.Tasks.Remove(model);
            await _dbContext.SaveChangesAsync();

            return true;
        }

        public async Task<List<TaskModel>> FindAllByUserId(int userId)
        {
            return await _dbContext.Tasks
                .Include(x => x.User)
                .Where(x => x.UserId == userId)
                .ToListAsync();
        }
    }
}
