using api.Dtos.Projects;
using api.Extensions;
using api.Interfaces.Projects;
using api.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace api.Controllers.Project
{
  [ApiController]
  [Route("api/projects")]
  [Authorize]
  public class ProjectController(IProjectService service, UserManager<AppUser> userManager) : ControllerBase
  {
    private readonly IProjectService _service = service;
    private readonly UserManager<AppUser> _userManager = userManager;

    [HttpGet]
    [Authorize(Roles = "Admin, Manager")]
    public async Task<IActionResult> List(string limit, string page, string? typeId, string? search)
    {
      if (User.IsInRole("Admin")) return Ok(await _service.PaginateAsync(int.Parse(limit), int.Parse(page), typeId, search));
      var manager = await _userManager.FindByEmailAsync(User.GetEmail());
      return Ok(await _service.PaginateAsync(int.Parse(limit), int.Parse(page), typeId, search, manager?.Id));
    }

    [HttpGet("{id}")]
    [Authorize(Roles = "Admin, Manager, Employee")]
    public async Task<IActionResult> Retrieve(string id)
    {
      var project = await _service.ReadAsync(id);
      if (User.IsInRole("Admin")) return Ok(project);
      var user = await _userManager.FindByEmailAsync(User.GetEmail());
      if (
        user?.Id == project?.ResponsibleId
        || project!.Members.Contains(user!.Id)
      ) return Ok(project);
      return Forbid();
    }

    [HttpPost]
    [Authorize(Roles = "Admin, Manager")]
    public async Task<IActionResult> New([FromBody] NewProjectDto projectType)
    {
      await _service.CreateAsync(projectType);
      return NoContent();
    }

    [HttpPut("{id}")]
    [Authorize(Roles = "Admin, Manager")]
    public async Task<IActionResult> Edit(string id, [FromBody] UpdateProjectDto updateProject)
    {
      if (User.IsInRole("Admin"))
      {
        await _service.UpdateAsync(id, updateProject);
      }
      else
      {
        var project = await _service.ReadAsync(id);
        var manager = await _userManager.FindByEmailAsync(User.GetEmail());
        if (project?.ResponsibleId == manager?.Id) await _service.UpdateAsync(id, updateProject);
      }
      return NoContent();
    }

    [HttpDelete("{id}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Delete(string id)
    {
      await _service.DeleteAsync(id);
      return NoContent();
    }

    [HttpPost("delete")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> MultiDelete([FromBody] List<string>? DeleteIds)
    {
      await _service.MultiDelete(DeleteIds);
      return NoContent();
    }

    [HttpGet("trash")]
    [Authorize(Roles = "Admin")] // Chỉ có admin mới được xem thùng rác
    public async Task<IActionResult> ListTrash(string limit, string page, string? typeId, string? search)
    {
      return Ok(await _service.ListTrash(int.Parse(limit), int.Parse(page), typeId, search));
    }

    [HttpPost("trash")]
    [Authorize(Roles = "Admin, Manager")] // Admin và Manager đều có quyền đưa item vào thùng rác.
    public async Task<IActionResult> MultiTrash([FromBody] List<string>? trashIds)
    {
      await _service.MultiTrash(trashIds);
      return NoContent();
    }

    [HttpGet("trash/{id}")]
    [Authorize(Roles = "Admin, Manager")]
    public async Task<IActionResult> Trash(string id)
    {
      await _service.TrashAsync(id);
      return NoContent();
    }

    [HttpPost("restore")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> MultiRestore([FromBody] List<string> restoreIds)
    {
      await _service.MultiRestore(restoreIds);
      return NoContent();
    }

    [HttpGet("restore/{id}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Restore(string id)
    {
      await _service.RestoreAsync(id);
      return NoContent();
    }
  }
}