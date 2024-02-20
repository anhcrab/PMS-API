
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace api.Models
{
  [Table("departments")]
  public class Department
  {
    [Key]
    public string Id { get; set; } = Guid.NewGuid().ToString();
    [Required]
    public string Name { get; set; } = null!;
    [Required]
    public string Address { get; set; } = null!;
    public List<Employee> Members { get; set; } = []; // Danh sách thành viên chỉ tồn tại 1 vị trí quản lý  
  }
}