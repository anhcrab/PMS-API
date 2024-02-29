using System.ComponentModel.DataAnnotations;

namespace api.Dtos.Core
{
  public class NewUserDto
  {
    [Required]
    public string UserName { get; set; } = null!;
    [Required]
    [EmailAddress]
    public string Email { get; set; } = null!;
    [Required]
    public string Password { get; set; } = null!;
  }
}