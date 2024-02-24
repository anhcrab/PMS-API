using System.ComponentModel.DataAnnotations;
using api.Helpers;

namespace api.Dtos
{
  public class NewEmployee
  {
    [Required]
    public string Id { get; set; } = null!;
    [Required]
    public string Department { get; set; } = null!;
    [Required]
    public string SupervisorId { get; set; } = null!;
  }
}