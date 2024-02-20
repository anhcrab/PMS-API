using api.Models;

namespace api.Interfaces
{
  public interface IDepartmentRepository
  {
    public Task<List<Department>?> GetAllAsync();
    public Task<Department?> GetAsync(string id);
    public Task<Department?> AddAsync(Department model);
    public Task UpdateAsync(string id, Department model);
    public Task DeleteAsync(string id);
  }
}