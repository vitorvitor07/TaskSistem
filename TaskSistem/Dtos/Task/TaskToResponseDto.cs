
using TaskSistem.Dtos.User;

namespace TaskSistem.Dtos.Task
{
    public class TaskToResponseDto
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public int Status { get; set; }
        public int? UserId { get; set; }

        public UserToResponseDto? User { get; set; }
    }
}