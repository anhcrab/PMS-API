using api.Models;

namespace api.Interfaces
{
  public interface IEmployeeRepository
  {
    public Task<List<Employee>?> GetAllAsync();
    public Task<Employee?> GetAsync(string id);
    public Task<Employee?> AddAsync(Employee model);
    public Task UpdateAsync(string id, Employee model);
    public Task DeleteAsync(string id);
    public Task<Employee?> GetByEmailAsync(string email);
  }
}