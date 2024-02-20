
using System.ComponentModel.DataAnnotations;

namespace api.Dtos
{
  public class RegisterEmployeeDto
  {
    [Required]
    public string UserId { get; set; } = null!;
    [Required]
    public string SupervisorId { get; set; } = null!;
    [Required]
    public string DepartmentId { get; set; } = null!;
  }
}