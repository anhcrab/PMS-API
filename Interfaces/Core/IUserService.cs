using api.Dtos.Core;

namespace api.Interfaces.Core
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