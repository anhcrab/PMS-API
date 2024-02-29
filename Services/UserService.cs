using api.Dtos.Core;
using api.Helpers;
using api.Interfaces.Core;
using api.Mappers;
using api.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace api.Services
{
  public class UserService(UserManager<AppUser> userManager) : IUserService
  {
    private readonly UserManager<AppUser> _userManager = userManager;
    public async Task<List<UserDto>> AllAsync()
    {
      var result = await _userManager.Users.ToListAsync();
      if (result.IsNullOrEmpty()) return [];
      return result.Select(u => u.ToUserDto()).ToList();
    }

    public async Task CreateAsync(NewUserDto newUserDto)
    {
      var newUser = new AppUser
      {
        UserName = newUserDto.UserName,
        Email = newUserDto.Email,
      };
      var createdUser = await _userManager.CreateAsync(newUser, newUserDto.Password);
      if (createdUser.Succeeded)
      {
        await _userManager.AddToRoleAsync(newUser, "CLIENT");
      }
    }

    public async Task DeleteAsync(string id)
    {
      var user = await _userManager.FindByIdAsync(id);
      if (user != null)
      {
        await _userManager.DeleteAsync(user);
      }
    }

    public async Task<UserDto> ReadAsync(string id)
    {
      var user = await _userManager.FindByIdAsync(id);
      return user!.ToUserDto();
    }

    public async Task UpdateAsync(string id, UserDto userDto)
    {
      var user = await _userManager.FindByIdAsync(id);
      if (user != null)
      {
        user.FirstName = userDto.FirstName ??= "";
        user.LastName = userDto.LastName ??= "";
        user.Sex = userDto.Sex ??= "";
        user.Dob = userDto.Dob ??= "";
        user.Hometown = userDto.Hometown ??= "";
        user.Department = ToDepartment(userDto.Department);
        user.Position = userDto.Position ??= "";
        user.PhoneNumber = userDto.PhoneNumber ??= "";
        user.SupervisorId = userDto.SupervisorId ??= "";
        user.Description = userDto.Description ??= "";
        user.AdditionalInfo = userDto.AdditionalInfo ??= "";
        user.Status = ToEmployeeStatus(userDto.Status);
        await _userManager.UpdateAsync(user);
      }
    }

    private static Departments ToDepartment(string str)
    {
      return str switch
      {
        "BUSINESS" => Departments.BUSINESS,
        "MARKETING" => Departments.MARKETING,
        "ADMINISTRATION" => Departments.ADMINISTRATION,
        "HR" => Departments.HR,
        "ACCOUNTING" => Departments.ACCOUNTING,
        _ => Departments.NONE,
      };
    }

    private static EmployeeStatuses ToEmployeeStatus(string str)
    { 
      return str switch
      {
        "EMPLOYED" => EmployeeStatuses.EMPLOYED,
        "FIRED" => EmployeeStatuses.FIRED,
        "RETIRED" => EmployeeStatuses.RETIRED,
        _ => EmployeeStatuses.FREE,
      };
    }
  }
}