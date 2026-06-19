using System.ComponentModel.DataAnnotations;

namespace TaskSistem.Dtos.User
{
    public class UpdateUserDto
    {
        [StringLength(100, MinimumLength = 3)]
        public required string Name { get; set; }
    }
}
