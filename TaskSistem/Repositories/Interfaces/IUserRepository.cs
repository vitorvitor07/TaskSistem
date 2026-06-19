using TaskSistem.Models;

namespace TaskSistem.Repositories.Interfaces {
    public interface IUserRepository {
        Task<List<UserModel>> FindAll();
        Task<UserModel> FindOne(int Id);
        Task<UserModel> Insert(UserModel User);
        Task<UserModel> Update(UserModel User, int Id);
        Task<bool> Delete(int Id);
        Task<UserModel> FindByEmail(string email);
        Task<bool> EmailExists(string email);
    }
}
