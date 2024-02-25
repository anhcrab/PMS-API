using api.Dtos;
using api.Helpers;

namespace api.Interfaces
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