using System.ComponentModel.DataAnnotations;
using TaskSistem.Models;

namespace TaskSistem.Dtos.Task
{
    public class UpdateTaskDto
    {
        [StringLength(150, MinimumLength = 3)]
        public string? Name { get; set; }

        [StringLength(1000)]
        public string? Description { get; set; }

        [Range(1, 3, ErrorMessage = "Invalid status")]
        public int? Status { get; set; }
    }
}
