using System.Text.RegularExpressions;
using api.Dtos.Projects;
using api.Interfaces.Projects;
using api.Mappers;
using api.Models;
using Microsoft.AspNetCore.Identity;

namespace api.Services
{
  public class WorkTaskService(
    UserManager<AppUser> userManager,
    IProjectRepository projectRepo,
    IWorkTaskRepository repository
  ) : IWorkTaskService
  {
    private readonly UserManager<AppUser> _userManager = userManager;
    private readonly IProjectRepository _projectRepo = projectRepo;
    private readonly IWorkTaskRepository _repository = repository;
    public async Task CreateAsync(NewWorkTaskDto newTask)
    {
      var usr = await _userManager.FindByIdAsync(newTask.MemberId);
      var pj = await _projectRepo.ReadAsync(newTask.ProjectId);
      if (usr != null && pj != null)
      {
        var task = new WorkTask
        {
          Name = newTask.Name,
          Content = newTask.Content ??= "",
          IsCompleted = newTask.IsCompleted,
          Deadline = newTask.Deadline,
          Project = pj,
          Member = usr
        };
        await _repository.CreateAsync(task);
      }
    }

    public async Task DeleteAsync(string id)
    {
      await _repository.DeleteAsync(id);
    }

    public async Task<List<WorkTaskDto>> ListAsync()
    {
      var list = await _repository.ListAsync();
      return list.Select(w => w.ToWorkTaskDto()).ToList();
    }

    public async Task<PaginateWorkTaskDto> ListTrashAsync(
      int limit = 20,
      int page = 1,
      string? projectId = null,
      string? search = null
    )
    {
      var list = await _repository.ListAsync();
      if (limit == -1)
      {
        return new PaginateWorkTaskDto
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
            .Select(p => p.ToWorkTaskDto())
            .ToList()
        };
      }
      var tasks = list
        .Where(p =>
        {
          if (p.DeletedDate == null) return false;
          var result = DateTime.Compare(p.DeletedDate ??= DateTime.Now, p.UpdatedDate);
          return result >= 0;
        })
        .Where(p => projectId == null || projectId == "" || p.ProjectId == projectId)
        .Where(p => search == null || search == "" || Regex.IsMatch(p.Name, Regex.Escape(search), RegexOptions.IgnoreCase))
        .ToList();
      var totalItems = tasks.Count;
      var totalPages = (int)Math.Ceiling(totalItems / (decimal)limit);
      var items = tasks
        .Skip((page - 1) * limit)
        .Take(limit)
        .Select(p => p.ToWorkTaskDto())
        .ToList();
      return new PaginateWorkTaskDto
      {
        TotalPages = totalPages,
        TotalItems = totalItems,
        Items = items
      };
    }

    public async Task<WorkTaskDto> MarkAsync(string id)
    {
      var workTask = await _repository.ReadAsync(id);
      workTask!.IsCompleted = !workTask!.IsCompleted;
      await _repository.UpdateAsync(id, workTask);
      return workTask.ToWorkTaskDto();
    }

    public async Task MultiDeleteAsync(List<string> ids)
    {
      for (var i = 0; i < ids.Count; i++)
      {
        await _repository.DeleteAsync(ids.ElementAt(i));
      }
    }

    public async Task MultiRestoreAsync(List<string> ids)
    {
      for (var i = 0; i < ids.Count; i++)
      {
        await RestoreAsync(ids.ElementAt(i));
      }
    }

    public async Task MultiTrashAsync(List<string> ids)
    {
      for (var i = 0; i < ids.Count; i++)
      {
        await TrashAsync(ids.ElementAt(i));
      }
    }

    public async Task<PaginateWorkTaskDto> PaginateAsync(
      int limit = 20,
      int page = 1,
      string? projectId = null,
      string? search = null,
      string? userId = null,
      bool isMember = false
    )
    {
      var list = await _repository.ListAsync();
      if (userId != null)
      {
        if (isMember) list = list.Where(w => w.MemberId == userId).ToList();
        else
        {
          var managerProjects = _projectRepo
            .ListAsync()
            .Result
            .Where(p => p.ResponsibleId == userId)
            .ToList();
          list = list.Where(w => managerProjects.Any(p => p.Id == w.ProjectId)).ToList();
        }
      }

      if (limit == -1)
      {
        return new PaginateWorkTaskDto
        {
          TotalItems = list.Count,
          TotalPages = 1,
          Items = list
            .Where(w =>
            {
              if (w.DeletedDate == null) return true;
              var result = DateTime.Compare(w.DeletedDate ??= DateTime.Now, w.UpdatedDate);
              return result < 0;
            })
            .Select(w => w.ToWorkTaskDto())
            .ToList()
        };
      }
      var tasks = list
        .Where(p =>
        {
          if (p.DeletedDate == null) return true;
          var result = DateTime.Compare(p.DeletedDate ??= DateTime.Now, p.UpdatedDate);
          return result < 0;
        })
        .Where(p => projectId == null || projectId == "" || p.ProjectId == projectId)
        .Where(p => search == null || search == "" || Regex.IsMatch(p.Name, Regex.Escape(search), RegexOptions.IgnoreCase))
        .ToList();
      var totalItems = tasks.Count;
      var totalPages = (int)Math.Ceiling(totalItems / (decimal)limit);
      var items = tasks
        .Skip((page - 1) * limit)
        .Take(limit)
        .Select(p => p.ToWorkTaskDto())
        .ToList();
      return new PaginateWorkTaskDto
      {
        TotalPages = 2,
        TotalItems = totalItems,
        Items = items
      };
    }

    public async Task<WorkTask?> ReadAsync(string id)
    {
      return await _repository.ReadAsync(id);
    }

    public async Task RestoreAsync(string id)
    {
      var task = await _repository.ReadAsync(id);
      if (task != null)
      {
        task.UpdatedDate = DateTime.Now;
        await _repository.UpdateAsync(id, task);
      }
    }

    public async Task TrashAsync(string id)
    {
      var task = await _repository.ReadAsync(id);
      if (task != null)
      {
        var now = DateTime.Now;
        task.UpdatedDate = now;
        task.DeletedDate = now;
        await _repository.UpdateAsync(id, task);
      }
    }

    public async Task UpdateAsync(string id, WorkTaskDto task, AppUser? manager = null)
    {
      var workTask = await _repository.ReadAsync(id);
      if (workTask != null)
      {
        if (manager != null)
        {
          var managerProj = await _projectRepo.ReadAsync(workTask.ProjectId);
          if (managerProj?.ResponsibleId == manager.Id)
          {
            workTask.Name = task.Name;
            workTask.IsCompleted = task.IsCompleted;
            workTask.Deadline = task.Deadline;
            workTask.Content = task.Content;
            workTask.MemberId = task.MemberId;
            workTask.ProjectId = task.ProjectId;
            workTask.UpdatedDate = DateTime.Now;
            await _repository.UpdateAsync(id, workTask);
          }
        }
        else
        {
          workTask.Name = task.Name;
          workTask.IsCompleted = task.IsCompleted;
          workTask.Deadline = task.Deadline;
          workTask.Content = task.Content;
          workTask.MemberId = task.MemberId;
          workTask.ProjectId = task.ProjectId;
          workTask.UpdatedDate = DateTime.Now;
          await _repository.UpdateAsync(id, workTask);
        }
      }
    }

    // private bool CanAccessTo()
    // {
    //   // _userManager.IsInRoleAsync()
    //   return true;
    // }

  }
}