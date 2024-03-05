using api.Dtos.Core;

namespace api.Interfaces.Core
{
  public interface IUserService
  {
    Task<List<UserDto>> AllAsync();
    Task<PaginateUserDto> PaginateAsync(int limit = 20, int page = 1, string? role = null, string? search = null);
    Task CreateAsync(NewUserDto newUserDto);
    Task<UserDto> ReadAsync(string id);
    Task UpdateAsync(string id, UserDto userDto);
    Task<PaginateUserDto> TrashView(int limit = 20, int page = 1, string? role = null, string? search = null);
    Task DeleteAsync(string id);
    Task MultiDeleteAsync(List<string>? ids);
    Task TrashAsync(string id);
    Task MultiTrashAsync(List<string>? ids);
    Task RestoreAsync(string id);
    Task MultiRestoreAsync(List<string>? ids);
  }
}