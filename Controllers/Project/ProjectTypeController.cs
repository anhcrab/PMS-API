using api.Dtos;
using api.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers.Project
{
  [ApiController]
  [Route("api/projecttypes")]
  [Authorize(Roles = "Admin")]
  public class ProjectTypeController(IProjectTypeService service) : ControllerBase
  {
    private readonly IProjectTypeService _service = service;

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
    public async Task<IActionResult> New([FromBody] ProjectTypeDto projectType)
    {
      await _service.CreateAsync(projectType);
      return NoContent();
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Edit(string id, [FromBody] ProjectTypeDto projectType)
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
  }
}