using api.Dtos;

namespace api.Interfaces
{
  public interface IEmployeeService
  {
    Task<List<EmployeeDto>> Paginate(int limit = 20, int page = 1, string search = "");
    Task<EmployeeDto?> Register(string userId, string supervisorId, string departmentId);
    Task<EmployeeDto?> FindById(string id);
    Task<List<EmployeeDto>?> All();
    Task<EmployeeDto?> FindByEmail(string email);
    Task<string?> GetRole(string email);
    Task Update(string id, UpdateEmployeeDto employeeDto);
    Task Delete(string id);
  }
}