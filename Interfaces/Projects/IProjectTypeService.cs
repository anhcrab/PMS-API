using api.Dtos.Projects;

namespace api.Interfaces.Projects
{
  public interface IProjectTypeService
  {
    Task<List<ProjectTypeDto>> ListAsync();
    Task CreateAsync(NewProjectTypeDto type);
    Task<ProjectTypeDto?> ReadAsync(string id);
    Task UpdateAsync(string id, ProjectTypeDto type);
    Task DeleteAsync(string id);
  }
}