using api.Models;

namespace api.Interfaces
{
  public interface IProjectTypeRepository
  {
    Task<List<ProjectType>> ListAsync();
    Task CreateAsync(ProjectType type);
    Task<ProjectType?> ReadAsync(string id);
    Task UpdateAsync(string id, ProjectType type);
    Task DeleteAsync(string id);
  }
}