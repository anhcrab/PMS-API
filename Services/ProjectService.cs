using api.Dtos;
using api.Helpers;
using api.Interfaces;
using api.Mappers;
using api.Models;

namespace api.Services
{
  public class ProjectService(IProjectRepository repository) : IProjectService
  {
    private readonly IProjectRepository _repository = repository;

    public async Task<List<ProjectDto>?> ListAsync()
    {
      var list = await _repository.ListAsync();
      return list.Select(p => p.ToProjectDto()).ToList();
    }

    public async Task CreateAsync(ProjectDto project)
    {
      var newProject = new Project
      {
        Name = project.Name ??= "",
        ResponsibleId = project.ResponsibleId ??= "",
        Progress = project.Progress ??= "",
        TypeId = project.TypeId ??= "",
        Budget = project.Budget,
        Deadline = project.Deadline ??= "",
        PaymentDate = project.PaymentDate ??= "",
        AdditionalInfo = project.AdditionalInfo ??= "",
        Status = ToProjectStatus(project.Status ??= "PENDING"),
        Created = DateTime.Now
      };
      await _repository.CreateAsync(newProject);
    }

    public async Task<ProjectDto?> ReadAsync(string id)
    {
      var project = await _repository.ReadAsync(id);
      return project?.ToProjectDto();
    }

    public async Task UpdateAsync(string id, ProjectDto project)
    {
      var existProject = await _repository.ReadAsync(id);
      if (existProject != null)
      {
        existProject.Name = project.Name ??= "";
        existProject.ResponsibleId = project.ResponsibleId ??= "";
        existProject.Progress = project.Progress ??= "";
        existProject.TypeId = project.TypeId ??= "";
        existProject.Budget = project.Budget;
        existProject.Deadline = project.Deadline ??= "";
        existProject.PaymentDate = project.PaymentDate ??= "";
        existProject.AdditionalInfo = project.AdditionalInfo ??= "";
        existProject.Status = ToProjectStatus(project.Status ??= "PENDING");
        await _repository.UpdateAsync(id, existProject);
      }
    }

    public async Task TrashAsync(string id)
    {
      var existProject = await _repository.ReadAsync(id);
      if (existProject != null)
      {
        existProject.Trash = true;
        await _repository.UpdateAsync(id, existProject);
      }
    }

    public async Task RestoreAsync(string id)
    {
      var existProject = await _repository.ReadAsync(id);
      if (existProject != null)
      {
        existProject.Trash = false;
        await _repository.DeleteAsync(id);
      }
    }

    public async Task DeleteAsync(string id)
    {
      var existProject = await _repository.ReadAsync(id);
      if (existProject != null)
      {
        await _repository.DeleteAsync(id);
      }
    }

    private static ProjectStatuses ToProjectStatus(string str)
    {
      return str switch
      {
        "ACTIVE" => ProjectStatuses.ACTIVE,
        "COMPLETE" => ProjectStatuses.COMPLETE,
        "CLOSED" => ProjectStatuses.CLOSED,
        _ => ProjectStatuses.PENDING,
      };
    }
  }
}