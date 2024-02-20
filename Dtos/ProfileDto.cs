using System.ComponentModel.DataAnnotations;

namespace api.Dtos
{
  public class ProfileDto
  {
    [Required]
    public string firstName { get; set; } = null!;
    [Required]
    public string lastName { get; set; } = null!;
    [Required]
    public string dob { get; set; } = null!;
    [Required]
    public string phoneNumber { get; set; } = null!;
    [Required]
    public string sex { get; set; } = null!;
    public string? description { get; set; }
  }
}