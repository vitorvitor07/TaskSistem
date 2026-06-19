
using TaskSistem.Dtos.User;

namespace TaskSistem.DTOs.Task
{
    public class TaskToResponseDTO
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public int Status { get; set; }
        public int UserId { get; set; }

        // Em vez de UserModel, usa o DTO limpo!
        public UserToResponseDto? User { get; set; }
    }
}