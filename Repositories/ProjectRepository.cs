using api.Databases;
using api.Interfaces.Projects;
using api.Models;
using Microsoft.EntityFrameworkCore;

namespace api.Repositories
{
  public class ProjectRepository(ApplicationDbContext context) : IProjectRepository
  {
    private readonly ApplicationDbContext _context = context;
    public async Task<List<Project>> ListAsync()
    {
      return await _context.Projects.Include(p => p.Members).ToListAsync();
    }

    public async Task CreateAsync(Project project)
    {
      _context.Add(project);
      await _context.SaveChangesAsync();
    }

    public async Task<Project?> ReadAsync(string id)
    {
      return await _context.Projects.Include(p => p.Members).FirstOrDefaultAsync(p => p.Id == id);
    }

    public async Task UpdateAsync(string id, Project project)
    {
      var existProject = await ReadAsync(id);
      if (existProject != null)
      {
        _context.Projects.Update(project);
        await _context.SaveChangesAsync();
      }
    }

    public async Task DeleteAsync(string id)
    {
      var project = await ReadAsync(id);
      if (project != null)
      {
        _context.Projects.Remove(project);
        await _context.SaveChangesAsync();
      }
    }

    // public Task UpdateMembers(Project project, List<AppUser> list)
    // {
    //   _context.TryUpdateManyToMany(project.Members, list.Select(u => new ProjectMember
    //   {
    //     ProjectId = project.Id,
    //     MemberId = u.Id
    //   }), x => x.MemberId);
    // }
  }
}