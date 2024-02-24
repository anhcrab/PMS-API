using api.Helpers;

namespace api.Dtos
{
  public class UserDto
  {
    public string? Id { get; set; }
    public string? UserName { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? Email { get; set; }
    public string? Sex { get; set; }
    public string? Dob { get; set; }
    public string? Hometown { get; set; }
    public Departments Department { get; set; }
    public string? Position { get; set; }
    public string? SupervisorId { get; set; }
    public string? Description { get; set; }
    public string? AdditionalInfo { get; set; }
    public EmployeeStatuses Status { get; set; }
  }
}