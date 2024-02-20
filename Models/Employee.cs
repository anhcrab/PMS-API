using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace api.Models
{
  [Table("employees")]
  public class Employee
  {
    [Key]
    public string EmployeeId { get; set; } = Guid.NewGuid().ToString();
    [Required]
    public string UserId { get; set; } = null!;
    [Required]
    public string SupervisorId { get; set; } = null!;
    [Required]
    public string DepartmentId { get; set; } = null!;
    [Required]
    public string Hometown { get; set; } = null!;
    [Required]
    public string Position { get; set; } = null!;
    public AppUser User { get; set; } = null!;
    public Employee Supervisor { get; set; } = null!;
    public Department Department { get; set; } = null!;
    public List<Employee> TeamMembers { get; set; } = []; 
  }
}