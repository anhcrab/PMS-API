using System.Text.RegularExpressions;
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
      var result = await _userManager.Users.Include(u => u.Projects).ToListAsync();
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
        user.Address = userDto.Address ??= "";
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

    public async Task<PaginateUserDto> PaginateAsync(
      int limit = 20,
      int page = 1,
      string? role = null,
      string? search = null
    )
    {
      var list = await _userManager.Users.Include(u => u.Projects).ToListAsync();
      if (role != null && role != "")
      {
        var ids = (await _userManager.GetUsersInRoleAsync(role)).Select(u => u.Id).ToList();
        list = list.Where(u => ids.Contains(u.Id)).ToList();
      }
      if (limit == -1)
      {
        return new PaginateUserDto
        {
          TotalItems = list.Count,
          TotalPages = 1,
          Items = list.Select(user => user.ToUserDto()).ToList()
        };
      }
      var users = list
        .Where(user =>
        {
          if (user.DeletedDate == null) return true;
          var result = DateTime.Compare(user.DeletedDate ??= DateTime.Now, user.UpdatedDate);
          return result < 0;
        })
        .Where(u => search == null || search == "" || Regex.IsMatch(u.UserName! + u.FirstName + u.LastName, Regex.Escape(search), RegexOptions.IgnoreCase))
        .ToList();
      var totalItems = users?.Count;
      var totalPages = (int)Math.Ceiling((decimal)(totalItems! / limit));
      var items = users?
        .Skip((page - 1) * limit)
        .Take(limit)
        .Select(u => u.ToUserDto())
        .ToList();
      return new PaginateUserDto
      {
        TotalPages = totalPages,
        TotalItems = (int)totalItems!,
        Items = items
      };
    }

    public async Task<PaginateUserDto> TrashView(int limit = 20, int page = 1, string? role = null, string? search = null)
    {
      var list = await _userManager.Users.ToListAsync();
      if (role != null)
      {
        list = [.. (await _userManager.GetUsersInRoleAsync(role))];
      }
      if (limit == -1)
      {
        return new PaginateUserDto
        {
          TotalItems = list.Count,
          TotalPages = 1,
          Items = list.Where(user =>
          {
            if (user.DeletedDate == null) return false;
            var result = DateTime.Compare(user.DeletedDate ??= DateTime.Now, user.UpdatedDate);
            return result > 0;
          }).Select(user => user.ToUserDto()).ToList()
        };
      }
      var users = list
        .Where(user =>
        {
          if (user.DeletedDate == null) return false;
          var result = DateTime.Compare(user.DeletedDate ??= DateTime.Now, user.UpdatedDate);
          return result > 0;
        })
        .Where(u => search == null || search == "" || Regex.IsMatch(u.UserName! + u.FirstName + u.LastName, Regex.Escape(search), RegexOptions.IgnoreCase))
        .ToList();
      var totalItems = users?.Count;
      var totalPages = (int)Math.Ceiling((decimal)(totalItems! / limit));
      var items = users?
        .Skip((page - 1) * limit)
        .Take(limit)
        .Select(u => u.ToUserDto())
        .ToList();
      return new PaginateUserDto
      {
        TotalPages = totalPages,
        TotalItems = (int)totalItems!,
        Items = items
      };
    }

    public async Task MultiDeleteAsync(List<string>? ids)
    {
      for (var i = 0; i < ids?.Count; i++)
      {
        var user = await _userManager.FindByIdAsync(ids.ElementAt(i));
        await _userManager.DeleteAsync(user!);
      }
    }

    public async Task TrashAsync(string id)
    {
      var user = await _userManager.FindByIdAsync(id);
      if (user != null)
      {
        user.DeletedDate = DateTime.Now;
        await _userManager.UpdateAsync(user);
      }
    }

    public async Task MultiTrashAsync(List<string>? ids)
    {
      for (var i = 0; i < ids?.Count; i++)
      {
        await TrashAsync(ids.ElementAt(i));
      }
    }

    public async Task RestoreAsync(string id)
    {
      var user = await _userManager.FindByIdAsync(id);
      if (user != null)
      {
        user.UpdatedDate = DateTime.Now;
        await _userManager.UpdateAsync(user);
      }
    }

    public async Task MultiRestoreAsync(List<string>? ids)
    {
      for (var i = 0; i < ids?.Count; i++)
      {
        await RestoreAsync(ids.ElementAt(i));
      }
    }
  }
}