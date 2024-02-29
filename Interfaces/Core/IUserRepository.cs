using api.Models;

namespace api.Interfaces.Core
{
  public interface IUserRepository
  {
    // Task CreateAsync(AppUser appUser);
    Task<List<AppUser>> AllAsync();
    // Task<AppUser> ReadAsync(string id);
    // Task UpdateAsync(string id, AppUser appUser);
    // Task DeleteAsync(string id);
  }
}