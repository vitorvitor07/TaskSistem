using Microsoft.EntityFrameworkCore;
using TaskSistem.Data;
using TaskSistem.Models;
using TaskSistem.Repositories.Interfaces;

namespace TaskSistem.Repositories {
    public class UserRepository : IUserRepository {
        private readonly TaskSistemDBContext _dbContext;

        public UserRepository(TaskSistemDBContext dbContext) {
            _dbContext = dbContext;
        }

        public async Task<List<UserModel>> FindAll() {
            return await _dbContext.Users.ToListAsync();
        }

        public async Task<UserModel> FindOne(int id) {
            return await _dbContext.Users.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<UserModel> Insert(UserModel user) {
            await _dbContext.Users.AddAsync(user);
            await _dbContext.SaveChangesAsync();

            return user;
        }

        public async Task<UserModel> Update(UserModel user, int id) {
            UserModel model = await FindOne(id) ?? throw new Exception($"User with id {id} not found");

            model.Name = user.Name;
            model.Email = user.Email;

            _dbContext.Users.Update(model);
            await _dbContext.SaveChangesAsync();

            return model;
        }

        public async Task<bool> Delete(int id) {
            UserModel model = await FindOne(id) ?? throw new Exception($"User with id {id} not found");

            _dbContext.Users.Remove(model);
            await _dbContext.SaveChangesAsync();

            return true;
        }

    }
}
