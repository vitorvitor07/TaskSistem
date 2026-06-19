using TaskSistem.Dtos.User;
using TaskSistem.Models;

namespace TaskSistem.Repositories.Interfaces
{
    public interface IUserRepository
    {
        Task<List<UserModel>> FindAll();
        Task<UserModel> FindOne(int Id);
        Task<UserModel> Insert(InsertUserDto data);
        Task<UserModel> UpdateAnyUser(int Id, UpdateUserDto data);
        Task<bool> Delete(int id);
        Task<UserModel> FindByEmail(string email);
        Task<bool> EmailExists(string email);
    }
}
