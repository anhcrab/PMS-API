using api.Dtos.Projects;
using api.Interfaces.Projects;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers.Project
{
  [ApiController]
  [Route("api/projects")]
  [Authorize]
  public class ProjectController(IProjectService service) : ControllerBase
  {
    private readonly IProjectService _service = service;

    [HttpGet]
    [Authorize(Roles = "Admin, Manager")]
    public async Task<IActionResult> List(string limit, string page, string? typeId, string? search)
    {
      return Ok(await _service.PaginateAsync(int.Parse(limit), int.Parse(page), typeId, search));
    }

    [HttpGet("{id}")]
    [Authorize(Roles = "Admin, Manager, Employee")]
    public async Task<IActionResult> Retrieve(string id)
    {
      return Ok(await _service.ReadAsync(id));
    }

    [HttpPost]
    [Authorize(Roles = "Admin, Manager")]
    public async Task<IActionResult> New([FromBody] ProjectDto projectType)
    {
      await _service.CreateAsync(projectType);
      return NoContent();
    }

    [HttpPut("{id}")]
    [Authorize(Roles = "Admin, Manager")]
    public async Task<IActionResult> Edit(string id, [FromBody] ProjectDto projectType)
    {
      await _service.UpdateAsync(id, projectType);
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
    [Authorize(Roles = "Admin, Manager")]
    public async Task<IActionResult> ListTrash(string limit, string page, string? typeId, string? search)
    {
      return Ok(await _service.ListTrash(int.Parse(limit), int.Parse(page), typeId, search));
    }

    [HttpPost("trash")]
    [Authorize(Roles = "Admin, Manager")]
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
    [Authorize(Roles = "Admin, Manager")]
    public async Task<IActionResult> MultiRestore([FromBody] List<string> restoreIds)
    {
      await _service.MultiRestore(restoreIds);
      return NoContent();
    }

    [HttpGet("restore/{id}")]
    [Authorize(Roles = "Admin, Manager")]
    public async Task<IActionResult> Restore(string id)
    {
      await _service.RestoreAsync(id);
      return NoContent();
    }
  }
}