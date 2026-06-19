using Mapster;
using Microsoft.AspNetCore.Mvc;
using TaskSistem.Dtos.User;
using TaskSistem.Models;
using TaskSistem.Repositories.Interfaces;
using TaskSistem.Services.Interfaces;

namespace TaskSistem.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<List<UserToResponseDto>> ListUsers()
        {
            List<UserModel> users = await _userRepository.FindAll();
            return users.Adapt<List<UserToResponseDto>>();
        }

        public async Task<UserToResponseDto> FindOne(int id)
        {
            UserModel user = await _userRepository.FindOne(id);
            return user.Adapt<UserToResponseDto>();
        }

        public async Task<UserToResponseDto> Insert(InsertUserDto data)
        {
            bool userExists = await _userRepository.EmailExists(data.Email);
            if (userExists) throw new Exception("Invalid email");
            data.Password = BCrypt.Net.BCrypt.HashPassword(data.Password);
            UserModel user = await _userRepository.Insert(data);
            return user.Adapt<UserToResponseDto>();
        }

        public async Task<UserToResponseDto> Update(int id, UpdateUserDto data)
        {
            UserModel user = await _userRepository.Update(id, data);
            return user.Adapt<UserToResponseDto>();
        }

        public async Task<bool> Delete(int id)
        {
            bool isDeleted = await _userRepository.Delete(id);
            return isDeleted;
        }
    }
}
