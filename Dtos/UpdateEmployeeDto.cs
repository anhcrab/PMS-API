using System.ComponentModel.DataAnnotations;

namespace api.Dtos
{
  public class UpdateEmployeeDto
  {
    [Required]
    public string DepartmentId { get; set; } = null!;
    [Required]
    public string SupervisorId { get; set; } = null!;
    [Required]
    public string Position { get; set; } = null!;
    [Required]
    public string Hometown { get; set; } = null!;
  }
}