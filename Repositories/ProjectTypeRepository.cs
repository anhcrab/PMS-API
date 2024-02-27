using api.Databases;
using api.Interfaces;
using api.Models;
using Microsoft.EntityFrameworkCore;

namespace api.Repositories
{
  public class ProjectTypeRepository(ApplicationDbContext context) : IProjectTypeRepository
  {
    private readonly ApplicationDbContext _context = context;

    public async Task<List<ProjectType>> ListAsync()
    {
      return await _context.ProjectTypes.ToListAsync();
    }

    public async Task CreateAsync(ProjectType type)
    {
      _context.Add(type);
      await _context.SaveChangesAsync();
    }

    public async Task<ProjectType?> ReadAsync(string id)
    {
      return await _context.ProjectTypes.FirstOrDefaultAsync(t => t.Id == id);
    }

    public async Task UpdateAsync(string id, ProjectType type)
    {
      var projectType = await ReadAsync(id);
      if (projectType != null)
      {
        _context.Update(projectType);
        await _context.SaveChangesAsync();
      }
    }

    public async Task DeleteAsync(string id)
    {
      var type = await ReadAsync(id);
      if (type != null)
      {
        _context.ProjectTypes.Remove(type);
        await _context.SaveChangesAsync();
      }
    }
  }
}