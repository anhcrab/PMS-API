using api.Dtos;

namespace api.Interfaces
{
  public interface IDepartmentService
  {
    Task<List<DepartmentDto>> GetAllAsync();
    Task<DepartmentDto> GetByIdAsync(string id);
    Task CreateAsync(DepartmentDto departmentDto);
    Task UpdateAsync(string id, DepartmentDto departmentDto);
    Task DeleteAsync(string id);
  } 
}