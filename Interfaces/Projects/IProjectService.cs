using api.Dtos.Project;

namespace api.Interfaces.Projects
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