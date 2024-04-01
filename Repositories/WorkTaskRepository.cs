using api.Databases;
using api.Interfaces.Projects;
using api.Models;
using Microsoft.EntityFrameworkCore;

namespace api.Repositories
{
  public class WorkTaskRepository(ApplicationDbContext context) : IWorkTaskRepository
  {
    private readonly ApplicationDbContext _context = context;
    public async Task CreateAsync(WorkTask task)
    {
      _context.Add(task);
      await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(string id)
    {
      var task = _context.WorkTasks.FirstOrDefault(t => t.Id == id);
      if (task != null)
      {
        _context.Remove(task);
        await _context.SaveChangesAsync();
      }
    }

    public async Task<List<WorkTask>> ListAsync()
    {
      return await _context.WorkTasks
        .Include(w => w.Member)
        .Include(w => w.Project)
        .ToListAsync();
    }

    public async Task<WorkTask?> ReadAsync(string id)
    {
      return await _context.WorkTasks.FirstOrDefaultAsync(w => w.Id == id);
    }

    public async Task UpdateAsync(string id, WorkTask task)
    {
      if (_context.WorkTasks.FirstOrDefault(w => w.Id == id) != null)
      {
        _context.WorkTasks.Update(task);
        await _context.SaveChangesAsync();
      }
    }
  }
}