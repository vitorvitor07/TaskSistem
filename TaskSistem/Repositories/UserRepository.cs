using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using TaskSistem.Data;
using TaskSistem.Dtos.User;
using TaskSistem.Models;
using TaskSistem.Repositories.Interfaces;
using static System.Runtime.InteropServices.JavaScript.JSType;

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
            if (data.Email == null) throw new Exception("Email is required");
            if (data.Password == null) throw new Exception("Password is required");

            bool userExists = await EmailExists(data.Email);

            if (userExists) throw new Exception("Invalid email");

            data.Password = BCrypt.Net.BCrypt.HashPassword(data.Password);

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

        public async Task<UserModel> UpdateAnyUser(int id, UpdateUserDto data)
        {
            UserModel model = await FindOne(id) ?? throw new Exception($"User with id {id} not found");

            model.Id = id;
            model.Name = data.Name;
            model.Email = data.Email;

            _dbContext.Users.Update(model);
            await _dbContext.SaveChangesAsync();

            return model;
        }

        public async Task<bool> Delete(int id)
        {
            UserModel model = await FindOne(id) ?? throw new Exception($"User with id {id} not found");

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
