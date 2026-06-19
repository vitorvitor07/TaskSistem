using System.ComponentModel.DataAnnotations;

namespace TaskSistem.Dtos.User
{
  public class InsertUserDto
  {
    [StringLength(100, MinimumLength = 3)]
    public required string Name { get; set; }

    [EmailAddress]
    public required string Email { get; set; }

    [StringLength(20, MinimumLength = 6)]
    public required string Password { get; set; }
  }
}