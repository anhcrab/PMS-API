using api.Dtos;
using api.Helpers;

namespace api.Interfaces
{
  public interface IEmployeeService
  {
    Task<List<UserDto>> AllAsync();
    Task<PaginateEmployeeDto> PaginateAsync(int paged);
    Task CreateAsync(string id, Departments department, string supervisorId);
    Task<UserDto> ReadAsync(string id);
    Task UpdateAsync(string id, UserDto userDto);
    Task DeleteAsync(string id);
  }
}