using TaskSistem.Dtos.User;

namespace TaskSistem.Services.Interfaces
{
    public interface IUserService
    {
        public Task<List<UserToResponseDto>> ListUsers();
        public Task<UserToResponseDto> FindOne(int id);
        public Task<UserToResponseDto> Insert(InsertUserDto data);
        public Task<UserToResponseDto> Update(int id, UpdateUserDto data);
        public Task<bool> Delete(int id);
    }
}
