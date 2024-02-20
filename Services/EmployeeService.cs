using api.Dtos;
using api.Interfaces;
using api.Mappers;
using api.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;

namespace api.Services
{
  public class EmployeeService(IEmployeeRepository repository, UserManager<AppUser> userManager) : IEmployeeService
  {
    private readonly IEmployeeRepository _repository = repository;
    private readonly UserManager<AppUser> _userManager = userManager;

    public Task<List<EmployeeDto>> Paginate(int limit = 20, int page = 1, string search = "")
    {
      throw new NotImplementedException();
    }

    public async Task<List<EmployeeDto>?> All()
    {
      var result = await _repository.GetAllAsync();
      if (result.IsNullOrEmpty()) return [];
      return result!.Select(x => x.ToEmployeeDto()).ToList();
    }

    public async Task<EmployeeDto?> FindById(string id)
    {
      var employee = await _repository.GetAsync(id);
      if (employee == null) return null;
      return employee.ToEmployeeDto();
    }

    public async Task<EmployeeDto?> FindByEmail(string email)
    {
      var employee = await _repository.GetByEmailAsync(email);
      if (employee == null) return null;
      return employee.ToEmployeeDto();
    }

    public async Task<string?> GetRole(string email)
    {
      var user = await _userManager.FindByEmailAsync(email);
      if (user == null) return null;
      var roles = await _userManager.GetRolesAsync(user);
      return roles.First();
    }

    public async Task<EmployeeDto?> Register(string userId, string supervisorId, string departmentId)
    {
      var user = await _userManager.FindByIdAsync(userId);
      if (user != null)
      {
        var employee = await _repository.AddAsync(new Employee
        {
          UserId = userId,
          SupervisorId = supervisorId,
          DepartmentId = departmentId
        });
        return employee!.ToEmployeeDto();
      }
      return null;
    }

    public async Task Update(string id, UpdateEmployeeDto employeeDto)
    {
      var employee = await _repository.GetAsync(id);
      if (employee != null)
      {
        employee.DepartmentId = employeeDto.DepartmentId;
        employee.SupervisorId = employeeDto.SupervisorId;
        employee.Position = employeeDto.Position;
        employee.Hometown = employeeDto.Hometown;
        await _repository.UpdateAsync(id, employee);
      }
    }

    public async Task Delete(string id)
    {
      await _repository.DeleteAsync(id);
    }
  }
}