using Microsoft.EntityFrameworkCore;
using TaskSistem.Data;
using TaskSistem.Dtos.User;
using TaskSistem.Models;
using TaskSistem.Repositories.Interfaces;

namespace TaskSistem.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly TaskSistemDBContext _dbContext;

        public UserRepository(TaskSistemDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<UserModel>> FindAll()
        {
            return await _dbContext.Users.ToListAsync();
        }

        public async Task<UserModel> FindOne(int id)
        {
            UserModel? model = await _dbContext.Users.FindAsync(id);
            if (model == null)
            {
                throw new Exception($"User with id {id} not found");
            }
            return model;
        }

        public async Task<UserModel> Insert(InsertUserDto data)
        {
            var model = new UserModel
            {
                Email = data.Email,
                Name = data.Name,
                Password = data.Password,
            };

            await _dbContext.Users.AddAsync(model);
            await _dbContext.SaveChangesAsync();

            return model;
        }

        public async Task<UserModel> Update(int id, UpdateUserDto data)
        {
            UserModel model = await FindOne(id);

            model.Name = data.Name;

            await _dbContext.SaveChangesAsync();

            return model;
        }

        public async Task<bool> Delete(int id)
        {
            UserModel model = await FindOne(id);
            _dbContext.Users.Remove(model);
            await _dbContext.SaveChangesAsync();

            return true;
        }

        public async Task<UserModel> FindByEmail(string email)
        {
            return await _dbContext.Users.FirstOrDefaultAsync(x => x.Email == email) ?? throw new Exception($"User with email {email} not found");
        }

        public async Task<bool> EmailExists(string email)
        {
            UserModel? model = await _dbContext.Users.FirstOrDefaultAsync(x => x.Email == email);
            return model != null;
        }
    }
}
