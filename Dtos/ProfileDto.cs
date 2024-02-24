using System.ComponentModel.DataAnnotations;

namespace api.Dtos
{
  public class ProfileDto
  {
    [Required]
    public string FirstName { get; set; } = null!;
    [Required]
    public string LastName { get; set; } = null!;
    [Required]
    public string Dob { get; set; } = null!;
    [Required]
    public string PhoneNumber { get; set; } = null!;
    [Required]
    public string Sex { get; set; } = null!;
    public string? Description { get; set; }
  }
}