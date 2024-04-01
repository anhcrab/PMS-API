using api.Dtos.Projects;
using api.Extensions;
using api.Interfaces.Projects;
using api.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers.Project
{
  [ApiController]
  [Route("api/tasks")]
  [Authorize]
  public class WorkTaskController(IWorkTaskService service, UserManager<AppUser> userManager) : ControllerBase
  {
    private readonly IWorkTaskService _service = service;
    private readonly UserManager<AppUser> _userManager = userManager;
    [HttpGet]
    [Authorize(Roles = "Admin, Manager, Employee")]
    public async Task<IActionResult> List(string limit, string page, string? projectId, string? search)
    {
      var response = await _service.PaginateAsync(int.Parse(limit), int.Parse(page), projectId, search);
      // Truy vấn từ Admin sẽ trả về toàn bộ kết quả.
      if (User.IsInRole("Admin")) return Ok(response);
      // Truy vấn từ Manager sẽ trả về kết quả những task trong phạm vi các dự án mà Manager chịu trách nhiệm.
      var user = await _userManager.FindByEmailAsync(User.GetEmail());
      if (User.IsInRole("Manager")) return Ok(response);
      // Truy vấn từ Employee sẽ trả về kết quả những task mà người này được giao.
      return Ok(await _service.PaginateAsync(int.Parse(limit), int.Parse(page), projectId, search, user?.Id, true));
    }
    [HttpPost]
    [Authorize(Roles = "Admin, Manager")]
    public async Task<IActionResult> Add([FromBody] NewWorkTaskDto newTask)
    {
      await _service.CreateAsync(newTask);
      return NoContent();
    }
    [HttpGet("{id}")]
    public async Task<IActionResult> Read(string id)
    {
      return Ok(await _service.ReadAsync(id));
    }

    [HttpGet("{id}/mark")]
    public async Task<IActionResult> Mark(string id)
    {
      return Ok(await _service.MarkAsync(id));
    }

    [HttpPut("{id}")]
    [Authorize(Roles = "Admin, Manager")]
    public async Task<IActionResult> Edit(string id, [FromBody] WorkTaskDto workTask)
    {
      if (User.IsInRole("Admin"))
      {
        await _service.UpdateAsync(id, workTask);
      }
      else
      {
        var user = await _userManager.FindByEmailAsync(User.GetEmail());
        await _service.UpdateAsync(id, workTask, user);
      }
      return NoContent();
    }
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(string id)
    {
      await _service.DeleteAsync(id);
      return NoContent();
    }
    [HttpPost("delete")]
    public async Task<IActionResult> MultiDelete([FromBody] List<string> ids)
    {
      await _service.MultiDeleteAsync(ids);
      return NoContent();
    }
    [HttpGet("trash")]
    public async Task<IActionResult> ListTrash(string limit, string page, string? projectId, string? search)
    {
      return Ok(await _service.ListTrashAsync(int.Parse(limit), int.Parse(page), projectId, search));
    }
    [HttpGet("trash/{id}")]
    public async Task<IActionResult> Trash(string id)
    {
      await _service.TrashAsync(id);
      return NoContent();
    }
    [HttpPost("trash")]
    public async Task<IActionResult> MultiTrash([FromBody] List<string> ids)
    {
      await _service.MultiTrashAsync(ids);
      return NoContent();
    }
    [HttpGet("restore/{id}")]
    public async Task<IActionResult> Restore(string id)
    {
      await _service.RestoreAsync(id);
      return NoContent();
    }
    [HttpPost("restore")]
    public async Task<IActionResult> MultiRestore([FromBody] List<string> ids)
    {
      await _service.MultiRestoreAsync(ids);
      return NoContent();
    }
  }
}