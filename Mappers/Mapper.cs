using api.Dtos;
using api.Models;
using Microsoft.AspNetCore.Identity;

namespace api.Mappers
{
  public static class Mapper
  {
    // public static EmployeeDto ToEmployeeDto(this AppUser employee)
    // {
    //   return new EmployeeDto
    //   {
    //     UserId = employee.Id,
    //     FirstName = employee.FirstName,
    //     LastName = employee.LastName,
    //     UserName = employee.UserName,
    //     Email = employee.Email,
    //     Sex = employee.Sex,
    //     Dob = employee.Dob,
    //     Description = employee.Description,
    //     AdditionalInfo = employee.AdditionalInfo,
    //     Hometown = employee.Hometown,
    //     // Position = employee.Position,
    //     // Supervisor = employee.Supervisor.ToEmployeeDto(),
    //     // Department = employee.Department.ToDepartmentDto(),
    //     // TeamMembers = employee.TeamMembers.Select(x => x.ToEmployeeDto()).ToList()
    //   };
    // }

    public static UserDto ToUserDto(this AppUser appUser)
    {
      return new UserDto {
        Id = appUser.Id,
        FirstName = appUser.FirstName,
        LastName = appUser.LastName,
        UserName = appUser.UserName,
        Email = appUser.Email,
        Sex = appUser.Sex,
        Dob = appUser.Dob,
        Description = appUser.Description,
        AdditionalInfo = appUser.AdditionalInfo,
        Hometown = appUser.Hometown,
        Position = appUser.Position,
        SupervisorId = appUser.SupervisorId,
        Department = appUser.Department,
        
      };
    }
    public static ProfileExtDto ToProfileExtDto(this AppUser appUser)
    {
      return new ProfileExtDto
      {
        Id = appUser.Id,
        UserName = appUser.UserName ??= "",
        Email = appUser.Email ??= ""
      };
    }
    public static RoleDto ToRoleDto(this IdentityRole role)
    {
      return new RoleDto
      {
        Id = role.Id,
        Name = role.Name!
      };
    }
  }
}