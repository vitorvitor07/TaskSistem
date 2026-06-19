using System.ComponentModel.DataAnnotations;

namespace TaskSistem.Dtos.Task
{
    public class InsertTaskDto
    {
        [StringLength(150, MinimumLength = 3)]
        public required string Name { get; set; }

        [StringLength(1000)]
        public string? Description { get; set; }

        [Range(1, 3, ErrorMessage = "Invalid status")]
        public required int Status { get; set; }
    }
}