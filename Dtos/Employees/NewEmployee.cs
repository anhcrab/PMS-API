using System.ComponentModel.DataAnnotations;

namespace api.Dtos.Employees
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