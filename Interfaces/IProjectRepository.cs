using api.Models;

namespace api.Interfaces
{
  public interface IProjectRepository
  {
    Task<List<Project>> ListAsync();
    Task CreateAsync(Project project);
    Task<Project?> ReadAsync(string id);
    Task UpdateAsync(string id, Project project);
    Task DeleteAsync(string id);
  }
}