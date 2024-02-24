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

    public async Task CreateAsync(string id, Departments department, string supervisorId)
    {
      var user = await _userManager.FindByIdAsync(id);
      if (user != null)
      {
        var isClient = await _userManager.IsInRoleAsync(user, "Client");
        if (isClient)
        {
          await _userManager.RemoveFromRoleAsync(user, "Client");
          await _userManager.AddToRoleAsync(user, "Employee");
          user.Position = "Nhân viên";
          user.Department = department;
          user.SupervisorId = supervisorId;
          user.Status = EmployeeStatuses.EMPLOYED;
          await _userManager.UpdateAsync(user);
        }
      }
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
      var employees = await AllAsync();
      employees = employees.Where(e => e.Status != EmployeeStatuses.FREE).ToList();
      var totalItems = employees.Count;
      var totalPages = (int) Math.Ceiling(totalItems / (decimal) limit);
      var items = employees
        .Skip((paged - 1) * limit)
        .Take(limit)
        .ToList();
      return new PaginateEmployeeDto
      {
        TotalPages = totalPages,
        TotalItems = totalItems,
        Items = items
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
  }
}