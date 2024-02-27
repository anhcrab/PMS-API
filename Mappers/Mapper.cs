using api.Dtos;
using api.Helpers;
using api.Models;
using Microsoft.AspNetCore.Identity;

namespace api.Mappers
{
  public static class Mapper
  {
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
        Department = appUser.Department.ToString(),
        Address = appUser.Address,
        PhoneNumber = appUser.PhoneNumber,
        Status = appUser.Status.ToString()
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
    public static ProjectTypeDto ToProjectTypeDto(this ProjectType type)
    {
      return new ProjectTypeDto
      {
        Id = type.Id,
        Name = type.Name,
        AdditionalInfo = type.AdditionalInfo
      };
    }
    public static ProjectDto ToProjectDto(this Project project)
    {
      return new ProjectDto
      {
        Id = project.Id,
        Name = project.Name,
        ResponsibleId = project.ResponsibleId,
        Progress = project.Progress,
        TypeId = project.TypeId,
        Budget = project.Budget,
        Deadline = project.Deadline,
        PaymentDate = project.PaymentDate,
        AdditionalInfo = project.AdditionalInfo,
        Status = project.Status.ToString(),
        Created = project.Created.ToString()
      };
    }
  }
}