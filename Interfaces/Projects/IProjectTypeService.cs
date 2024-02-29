using api.Dtos.Project;

namespace api.Interfaces.Projects
{
  public interface IProjectTypeService
  {
    Task<List<ProjectTypeDto>?> ListAsync();
    Task CreateAsync(ProjectTypeDto type);
    Task<ProjectTypeDto?> ReadAsync(string id);
    Task UpdateAsync(string id, ProjectTypeDto type);
    Task DeleteAsync(string id);
  }
}