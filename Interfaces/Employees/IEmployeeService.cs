using api.Dtos.Core;
using api.Dtos.Employees;

namespace api.Interfaces.Employees
{
  public interface IEmployeeService
  {
    Task<List<UserDto>> AllAsync();
    Task<PaginateEmployeeDto> PaginateAsync(int paged);
    Task<UserDto?> CreateAsync(string id, string department, string supervisorId);
    Task PromoteAsync(string id);
    Task DemoteAsync(string id);
    Task<UserDto> ReadAsync(string id);
    Task UpdateAsync(string id, UserDto userDto);
    Task DeleteAsync(string id);
    Task<List<UserDto>> Room(string email);
    Task<List<UserDto>> Team(string email);
  }
}