using System.ComponentModel.DataAnnotations;

namespace api.Dtos
{
  public class ForgotDto
  {
    [Required]
    [EmailAddress]
    public string? EmailAddress { get; set; }
  }
}