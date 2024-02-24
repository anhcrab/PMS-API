using api.Databases;
using api.Interfaces;
using api.Models;
using Microsoft.EntityFrameworkCore;

namespace api.Repositories
{
  public class UserRepository(ApplicationDbContext context) : IUserRepository
  {
    private readonly ApplicationDbContext _context = context;

    public async Task<List<AppUser>> AllAsync()
    {
      return await _context.Users.ToListAsync();
    }

    // public Task CreateAsync(AppUser appUser)
    // {
    //   throw new NotImplementedException();
    // }

    // public Task DeleteAsync(string id)
    // {
    //   throw new NotImplementedException();
    // }

    // public Task<AppUser> ReadAsync(string id)
    // {
    //   throw new NotImplementedException();
    // }

    // public Task UpdateAsync(string id, AppUser appUser)
    // {
    //   throw new NotImplementedException();
    // }
  }
}