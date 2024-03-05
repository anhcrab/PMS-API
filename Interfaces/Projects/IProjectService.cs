using api.Dtos.Projects;

namespace api.Interfaces.Projects
{
  public interface IProjectService
  {
    Task<List<ProjectDto>?> ListAsync();
    Task<PaginateProjectDto?> PaginateAsync(int limit = 20, int page = 1, string? typeId = null, string? search = null);
    Task CreateAsync(ProjectDto project);
    Task<ProjectDto?> ReadAsync(string id);
    Task UpdateAsync(string id, ProjectDto project);
    Task DeleteAsync(string id);
    Task MultiDelete(List<string>? deleteIds);
    Task TrashAsync(string id);
    Task MultiTrash(List<string>? trashIds);
    Task RestoreAsync(string id);
    Task MultiRestore(List<string>? restoreIds);
    Task<PaginateProjectDto?> ListTrash(int limit = 20, int page = 1, string? typeId = null, string? search = null);
  }
}