using api.Dtos;
using api.Helpers;
using api.Interfaces;
using api.Mappers;
using api.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace api.Services
{
  public class EmployeeService(UserManager<AppUser> userManager) : IEmployeeService
  {
    private readonly UserManager<AppUser> _userManager = userManager;

    public async Task<List<UserDto>> AllAsync()
    {
      var users = await _userManager.Users.Where(u => u.Status != EmployeeStatuses.FREE).ToListAsync();
      if (users.IsNullOrEmpty()) return [];
      return users.Select(u => u.ToUserDto()).ToList();
    }

    public async Task<UserDto?> CreateAsync(string id, string department, string supervisorId)
    {
      var user = await _userManager.FindByIdAsync(id);
      if (user != null)
      {
        var isClient = await _userManager.IsInRoleAsync(user, "Client");
        if (
          isClient
          || user.Status == EmployeeStatuses.FREE
          || user.Status == EmployeeStatuses.FIRED
          || user.Status == EmployeeStatuses.RETIRED
        )
        {
          await _userManager.RemoveFromRoleAsync(user, "Client");
          await _userManager.AddToRoleAsync(user, "Employee");
          user.Position = "Nhân viên";
          user.Status = EmployeeStatuses.EMPLOYED;
          user.Department = ToDepartment(department);
          user.SupervisorId = supervisorId;
          await _userManager.UpdateAsync(user);
          return user.ToUserDto();
        }
      }
      return null;
    }

    public async Task DeleteAsync(string id)
    {
      var user = await _userManager.FindByIdAsync(id);
      if (user != null)
      {
        var isEmployee = await _userManager.IsInRoleAsync(user, "Employee");
        var isManager = await _userManager.IsInRoleAsync(user, "Manager");
        if (isEmployee)
        {
          await _userManager.RemoveFromRoleAsync(user, "Employee");
          await _userManager.AddToRoleAsync(user, "Client");
        }
        if (isManager)
        {
          await _userManager.RemoveFromRoleAsync(user, "Manager");
          await _userManager.AddToRoleAsync(user, "Client");
        }
        user.Status = EmployeeStatuses.FIRED;
        await _userManager.UpdateAsync(user);
      }
    }

    public async Task<PaginateEmployeeDto> PaginateAsync(int paged)
    {
      int limit = 20;
      var employees = await _userManager.Users
        .Where(u => u.Status == EmployeeStatuses.EMPLOYED)
        .ToListAsync();
      var totalItems = employees.Count;
      var totalPages = (int)Math.Ceiling(totalItems / (decimal)limit);
      var items = employees
        .Skip((paged - 1) * limit)
        .Take(limit)
        .ToList();
      return new PaginateEmployeeDto
      {
        TotalPages = totalPages,
        TotalItems = totalItems,
        Items = items.Select(i => i.ToUserDto()).ToList()
      };
    }

    public Task<UserDto> ReadAsync(string id)
    {
      throw new NotImplementedException();
    }

    public Task UpdateAsync(string id, UserDto userDto)
    {
      throw new NotImplementedException();
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

    public async Task PromoteAsync(string id)
    {
      var user = await _userManager.FindByIdAsync(id);
      if (user != null)
      {
        await _userManager.RemoveFromRoleAsync(user, "Employee");
        await _userManager.AddToRoleAsync(user, "Manager");
        user.Position = "Quản lý";
        await _userManager.UpdateAsync(user);
      }
    }

    public async Task DemoteAsync(string id)
    {
      var user = await _userManager.FindByIdAsync(id);
      if (user != null)
      {
        await _userManager.RemoveFromRoleAsync(user, "Manager");
        await _userManager.AddToRoleAsync(user, "Employee");
        user.Position = "Nhân viên";
        await _userManager.UpdateAsync(user);
      }
    }

    public async Task<List<UserDto>> Room(string email)
    {
      var manager = await _userManager.FindByEmailAsync(email);
      var department = manager!.Department;
      var employees = await _userManager.Users
        .Where(u => u.Status == EmployeeStatuses.EMPLOYED)
        .Where(u => u.Department == department)
        .Select(u => u.ToUserDto())
        .ToListAsync();
      return employees;
    }

    public async Task<List<UserDto>> Team(string email)
    {
      var user = await _userManager.FindByEmailAsync(email);
      return await _userManager.Users
        .Where(u => u.SupervisorId == user!.SupervisorId)
        .Select(u => u.ToUserDto())
        .ToListAsync();
    }
  }
}