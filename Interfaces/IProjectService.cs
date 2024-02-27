using api.Dtos;

namespace api.Interfaces
{
  public interface IProjectService
  {
    Task<List<ProjectDto>?> ListAsync();
    Task CreateAsync(ProjectDto project);
    Task<ProjectDto?> ReadAsync(string id);
    Task UpdateAsync(string id, ProjectDto project);
    Task DeleteAsync(string id);
    Task TrashAsync(string id);
    Task RestoreAsync(string id);
  }
}