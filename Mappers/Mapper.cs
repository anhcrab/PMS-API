using api.Dtos;
using api.Models;

namespace api.Mappers
{
  public static class Mapper
  {
    public static EmployeeDto ToEmployeeDto(this Employee employee)
    {
      return new EmployeeDto
      {
        EmployeeId = employee.EmployeeId,
        UserId = employee.UserId,
        FirstName = employee.User.FirstName,
        LastName = employee.User.LastName,
        UserName = employee.User.UserName,
        Email = employee.User.Email,
        Sex = employee.User.Sex,
        Dob = employee.User.Dob,
        Description = employee.User.Description,
        AdditionalInfo = employee.User.AdditionalInfo,
        Hometown = employee.Hometown,
        Position = employee.Position,
        Supervisor = employee.Supervisor.ToEmployeeDto(),
        Department = employee.Department.ToDepartmentDto(),
        TeamMembers = employee.TeamMembers.Select(x => x.ToEmployeeDto()).ToList()
      };
    }

    public static DepartmentDto ToDepartmentDto(this Department department)
    {
      return new DepartmentDto
      {
        Id = department.Id,
        Name = department.Name,
        Address = department.Address,
        Members = department.Members.Select(x => x.ToEmployeeDto()).ToList()
      };
    }
  }
}