using Microsoft.EntityFrameworkCore;
using TaskSistem.Data;
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

        public async Task<UserModel> Insert(UserModel user)
        {
            if (user.Email == null) throw new Exception("Email is required");
            if (user.Password == null) throw new Exception("Password is required");

            bool userExists = await EmailExists(user.Email);

            if (userExists) throw new Exception("Invalid email");

            user.Password = BCrypt.Net.BCrypt.HashPassword(user.Password);

            await _dbContext.Users.AddAsync(user) ;
            await _dbContext.SaveChangesAsync();

            return user;
        }

        public async Task<UserModel> Update(UserModel user, int id)
        {
            UserModel model = await FindOne(id) ?? throw new Exception($"User with id {id} not found");

            model.Name = user.Name;
            model.Email = user.Email;

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
