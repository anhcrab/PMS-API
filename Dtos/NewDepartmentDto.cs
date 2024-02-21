
using System.ComponentModel.DataAnnotations;

namespace api.Dtos
{
  public class NewDepartmentDto
  {
    [Required]
    public string Name { get; set; } = null!;
    [Required]
    public string Address { get; set; } = null!;
  }
}