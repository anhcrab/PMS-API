using api.Dtos;

namespace api.Interfaces
{
  public interface IUserService
  {
    Task<List<UserDto>> AllAsync();
    Task CreateAsync(NewUserDto newUserDto);
    Task<UserDto> ReadAsync(string id);
    Task UpdateAsync(string id, UserDto userDto);
    Task DeleteAsync(string id);
  }
}