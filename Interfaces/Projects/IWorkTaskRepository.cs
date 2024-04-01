using api.Models;

namespace api.Interfaces.Projects
{
  public interface IWorkTaskRepository
  {
    Task<List<WorkTask>> ListAsync();
    Task CreateAsync(WorkTask task);
    Task<WorkTask?> ReadAsync(string id);
    Task UpdateAsync(string id, WorkTask task);
    Task DeleteAsync(string id);
  }
}