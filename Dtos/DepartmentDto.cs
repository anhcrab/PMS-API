using System.ComponentModel.DataAnnotations;
using api.Models;

namespace api.Dtos
{
  public class DepartmentDto
  {
    public string? Id { get; set; }
    [Required]
    public string Name { get; set; } = null!;
    [Required]
    public string Address { get; set; } = null!;
    public List<EmployeeDto>? Members { get; set; }
  }
}