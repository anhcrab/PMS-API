using api.Models;

namespace api.Interfaces.Projects
{
  public interface IProjectRepository
  {
    Task<List<Project>> ListAsync();
    Task CreateAsync(Project project);
    Task<Project?> ReadAsync(string id);
    Task UpdateAsync(string id, Project project);
    Task DeleteAsync(string id);
    // Task UpdateMembers(Project project, List<AppUser> list);
  }
}