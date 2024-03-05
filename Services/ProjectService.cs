using System.Text.RegularExpressions;
using api.Dtos.Projects;
using api.Helpers;
using api.Interfaces.Projects;
using api.Mappers;
using api.Models;
using Microsoft.AspNetCore.Identity;

namespace api.Services
{
  public class ProjectService(
    IProjectRepository repository,
    UserManager<AppUser> userManager,
    IProjectTypeRepository typeRepo
  ) : IProjectService
  {
    private readonly IProjectRepository _repository = repository;
    private readonly UserManager<AppUser> _userManager = userManager;
    private readonly IProjectTypeRepository _typeRepo = typeRepo;

    public async Task<List<ProjectDto>?> ListAsync()
    {
      var list = await _repository.ListAsync();
      return list.Select(p => p.ToProjectDto()).ToList();
    }

    public async Task CreateAsync(ProjectDto project)
    {
      var time = DateTime.Now;
      var type = await _typeRepo.ReadAsync(project.TypeId!);
      if (type != null)
      {
        var newProject = new Project
        {
          Name = project.Name ??= "",
          Progress = project.Progress ??= "",
          ResponsibleId = project.ResponsibleId ??= "",
          Type = type,
          Budget = project.Budget,
          Deadline = project.Deadline ??= "",
          PaymentDate = project.PaymentDate ??= "",
          AdditionalInfo = project.AdditionalInfo ??= "",
          Status = ToProjectStatus(project.Status ??= "NEW"),
          CreationDate = time,
          UpdatedDate = time
        };
        await _repository.CreateAsync(newProject);
        type.Projects.Add(newProject);
        await _typeRepo.UpdateAsync(newProject.TypeId, type);
      }
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
        existProject.UpdatedDate = DateTime.Now;
        await _repository.UpdateAsync(id, existProject);
      }
    }

    public async Task TrashAsync(string id)
    {
      var existProject = await _repository.ReadAsync(id);
      if (existProject != null)
      {
        existProject.DeletedDate = DateTime.Now;
        await _repository.UpdateAsync(id, existProject);
      }
    }

    public async Task RestoreAsync(string id)
    {
      var existProject = await _repository.ReadAsync(id);
      if (existProject != null)
      {
        existProject.UpdatedDate = DateTime.Now;
        await _repository.UpdateAsync(id, existProject);
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
        "COMPLETED" => ProjectStatuses.COMPLETED,
        "CLOSED" => ProjectStatuses.CLOSED,
        _ => ProjectStatuses.NEW,
      };
    }

    public async Task<PaginateProjectDto?> PaginateAsync(
      int limit = 20,
      int page = 1,
      string? typeId = null,
      string? search = null
    )
    {
      var list = await _repository.ListAsync();
      if (limit == -1)
      {
        return new PaginateProjectDto
        {
          TotalItems = list.Count,
          TotalPages = 1,
          Items = list.Select(p => p.ToProjectDto()).ToList()
        };
      }
      var projects = list
        .Where(p =>
        {
          if (p.DeletedDate == null) return true;
          var result = DateTime.Compare(p.DeletedDate ??= DateTime.Now, p.UpdatedDate);
          return result < 0;
        })
        .Where(p => typeId == null || typeId == "" || p.TypeId == typeId)
        .Where(p => search == null || search == "" || Regex.IsMatch(p.Name, Regex.Escape(search), RegexOptions.IgnoreCase))
        .ToList();
      var totalItems = projects.Count;
      var totalPages = (int)Math.Ceiling(totalItems / (decimal)limit);
      var items = projects
        .Skip((page - 1) * limit)
        .Take(limit)
        .Select(p => p.ToProjectDto())
        .ToList();
      return new PaginateProjectDto
      {
        TotalPages = totalPages,
        TotalItems = totalItems,
        Items = items
      };
    }

    public async Task<PaginateProjectDto?> ListTrash(
      int limit = 20,
      int page = 1,
      string? typeId = null,
      string? search = null
    )
    {
      var list = await _repository.ListAsync();
      if (limit == -1)
      {
        return new PaginateProjectDto
        {
          TotalItems = list.Count,
          TotalPages = 1,
          Items = list
            .Where(p =>
            {
              if (p.DeletedDate == null) return false;
              var result = DateTime.Compare(p.DeletedDate ??= DateTime.Now, p.UpdatedDate);
              return result >= 0;
            })
            .Select(p => p.ToProjectDto())
            .ToList()
        };
      }
      var projects = list
        .Where(p =>
        {
          if (p.DeletedDate == null) return false;
          var result = DateTime.Compare(p.DeletedDate ??= DateTime.Now, p.UpdatedDate);
          return result >= 0;
        })
        .Where(p => typeId == null || typeId == "" || p.TypeId == typeId)
        .Where(p => search == null || search == "" || Regex.IsMatch(p.Name, Regex.Escape(search), RegexOptions.IgnoreCase))
        .ToList();
      var totalItems = projects.Count;
      var totalPages = (int)Math.Ceiling(totalItems / (decimal)limit);
      var items = projects
        .Skip((page - 1) * limit)
        .Take(limit)
        .Select(p => p.ToProjectDto())
        .ToList();
      return new PaginateProjectDto
      {
        TotalPages = totalPages,
        TotalItems = totalItems,
        Items = items
      };
    }

    public async Task MultiDelete(List<string>? deleteIds)
    {
      for (var i = 0; i < deleteIds?.Count; i++)
      {
        await _repository.DeleteAsync(deleteIds.ElementAt(i));
      }
    }

    public async Task MultiTrash(List<string>? trashIds)
    {
      for (var i = 0; i < trashIds?.Count; i++)
      {
        await TrashAsync(trashIds.ElementAt(i));
      }
    }

    public async Task MultiRestore(List<string>? restoreIds)
    {
      for (var i = 0; i < restoreIds?.Count; i++)
      {
        await RestoreAsync(restoreIds.ElementAt(i));
      }
    }
  }
}