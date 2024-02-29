using System.ComponentModel.DataAnnotations;

namespace api.Dtos.Core
{
  public class ForgotDto
  {
    [Required]
    [EmailAddress]
    public string? EmailAddress { get; set; }
  }
}