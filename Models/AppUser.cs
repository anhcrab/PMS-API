using Microsoft.AspNetCore.Identity;
using api.Helpers;

namespace api.Models
{
  public class AppUser : IdentityUser
  {
    public string FirstName { get; set; } = "";
    public string LastName { get; set; } = "";
    public string Sex { get; set; } = "male";
    public string Dob { get; set; } = "";
    public string Hometown { get; set; } = "";
    public string Address { get; set; } = "";
    public Departments Department { get; set; } = Departments.NONE;
    public string? SupervisorId { get; set; }
    public string Position { get; set; } = "";
    public string Description { get; set; } = "";
    public string AdditionalInfo { get; set; } = "";
    public EmployeeStatuses Status { get; set; } = EmployeeStatuses.FREE;
    public DateTime CreationDate { get; set; } = DateTime.Now;
    public DateTime UpdatedDate { get; set; } = DateTime.Now;
    public DateTime? DeletedDate { get; set; }
    public ICollection<Project> Projects { get; set; } = [];
    public ICollection<WorkTask> Tasks { get; set; } = [];
    public ICollection<ProjectMember> ProjectMembers { get; set; } = [];
  }
}