using api.Dtos.Project;
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
    public async Task<IActionResult> List()
    {
      return Ok(await _service.ListAsync());
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> Retrieve(string id)
    {
      return Ok(await _service.ReadAsync(id));
    }

    [HttpPost]
    public async Task<IActionResult> New([FromBody] ProjectDto projectType)
    {
      await _service.CreateAsync(projectType);
      return NoContent();
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Edit(string id, [FromBody] ProjectDto projectType)
    {
      await _service.UpdateAsync(id, projectType);
      return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(string id)
    {
      await _service.DeleteAsync(id);
      return NoContent();
    }

    [HttpGet("trash/{id}")]
    public async Task<IActionResult> Trash(string id)
    {
      await _service.TrashAsync(id);
      return NoContent();
    }

    [HttpGet("restore/{id}")]
    public async Task<IActionResult> Restore(string id)
    {
      await _service.RestoreAsync(id);
      return NoContent();
    }
  }
}