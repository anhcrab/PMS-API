using api.Models;

namespace api.Dtos
{
  public class EmployeeDto
  {
    public string? EmployeeId { get; set; }
    public string? UserName { get; set; }
    public string? Email { get; set; }
    public string? UserId { get; set; }
    public string? Hometown { get; set; }
    public string? Position { get; set; }
    public string? Description { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? Sex { get; set; }
    public string? Dob { get; set; }
    public string? AdditionalInfo { get; set; }
    public EmployeeDto? Supervisor { get; set; }
    public DepartmentDto? Department { get; set; }
    public List<EmployeeDto> TeamMembers { get; set; } = [];
  }
}