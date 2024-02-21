using api.Dtos;
using api.Interfaces;
using api.Mappers;
using api.Models;

namespace api.Services
{
  public class DepartmentService(IDepartmentRepository repository) : IDepartmentService
  {
    private readonly IDepartmentRepository _repository = repository;
    // private readonly ILogger<DepartmentService> _logger = logger;
    public async Task CreateAsync(NewDepartmentDto departmentDto)
    {
      var newDepartment = new Department
      {
        Name = departmentDto.Name,
        Address = departmentDto.Address,
      };
      // _logger.LogInformation(newDepartment.ToString());
      await _repository.AddAsync(newDepartment);
    }

    public async Task DeleteAsync(string id)
    {
      await _repository.DeleteAsync(id);
    }

    public async Task<List<DepartmentDto>> GetAllAsync()
    {
      var list = await _repository.GetAllAsync();
      return list!.Select(item => item.ToDepartmentDto()).ToList();
    }

    public async Task<DepartmentDto> GetByIdAsync(string id)
    {
      var department = await _repository.GetAsync(id);
      return department!.ToDepartmentDto();
    }

    public async Task UpdateAsync(string id, DepartmentDto departmentDto)
    {
      var department = await _repository.GetAsync(id);
      if (department != null) 
      {
        department.Address = departmentDto.Address;
        department.Name = departmentDto.Name;
      }
    }
  }
}