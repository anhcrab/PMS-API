using api.Dtos;
using api.Interfaces;
using api.Mappers;
using api.Models;

namespace api.Services
{
  public class ProjectTypeService(IProjectTypeRepository repository) : IProjectTypeService
  {
    private readonly IProjectTypeRepository _repository = repository;

    public async Task<List<ProjectTypeDto>?> ListAsync()
    {
      var list = await _repository.ListAsync();
      return list.Select(t => t.ToProjectTypeDto()).ToList();
    }

    public async Task CreateAsync(ProjectTypeDto type)
    {
      var newType = new ProjectType
      {
        Name = type.Name ??= "",
        AdditionalInfo = type.AdditionalInfo ??= ""
      };
      await _repository.CreateAsync(newType);
    }

    public async Task<ProjectTypeDto?> ReadAsync(string id)
    {
      var type = await _repository.ReadAsync(id);
      return type?.ToProjectTypeDto();
    }

    public async Task UpdateAsync(string id, ProjectTypeDto type)
    {
      var projectType = await _repository.ReadAsync(id);
      if (projectType != null)
      {
        projectType.Name = type.Name ??= "";
        projectType.AdditionalInfo = type.AdditionalInfo ??= "";
        await _repository.UpdateAsync(id, projectType);
      }
    }

    public async Task DeleteAsync(string id)
    {
      await _repository.DeleteAsync(id);
    }
  }
}