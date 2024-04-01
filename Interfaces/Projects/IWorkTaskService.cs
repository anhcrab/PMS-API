using api.Dtos.Projects;
using api.Models;

namespace api.Interfaces.Projects
{
  public interface IWorkTaskService
  {
    // CRUD
    Task<List<WorkTaskDto>> ListAsync();
    Task CreateAsync(NewWorkTaskDto newTask);
    Task<WorkTask?> ReadAsync(string id);
    Task UpdateAsync(string id, WorkTaskDto task, AppUser? manager = null);
    Task DeleteAsync(string id);
    // Additional Method: Paginate, Trash, Restore, ...
    Task MultiDeleteAsync(List<string> ids);
    Task<PaginateWorkTaskDto> PaginateAsync(
      int limit = 20, 
      int page = 1, 
      string? projectId = null, 
      string? search = null, 
      string? userId = null, 
      bool isMember = false
    );
    Task<PaginateWorkTaskDto> ListTrashAsync(
      int limit = 20, 
      int page = 1, 
      string? projectId = null, 
      string? search = null
    );
    Task TrashAsync(string id);
    Task MultiTrashAsync(List<string> ids);
    Task RestoreAsync(string id);
    Task MultiRestoreAsync(List<string> ids);
    Task<WorkTaskDto> MarkAsync(string id);
  }
}