using api.Databases;
using api.Dtos.Projects;
using api.Interfaces.Projects;
using api.Mappers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace api.Controllers.Project
{
  [ApiController]
  [Route("api/projecttypes")]
  [Authorize]
  public class ProjectTypeController(IProjectTypeService service, ApplicationDbContext context) : ControllerBase
  {
    private readonly IProjectTypeService _service = service;
    private readonly ApplicationDbContext _ctx = context;

    [HttpGet]
    public async Task<IActionResult> List()
    {
      // return Ok(await _ctx.ProjectTypes.Include(t => t.Projects).ToListAsync());
      return Ok(await _service.ListAsync());
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> Retrieve(string id)
    {
      return Ok(await _service.ReadAsync(id));
    }

    [HttpPost]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> New([FromBody] NewProjectTypeDto projectType)
    {
      await _service.CreateAsync(projectType);
      return NoContent();
    }

    [HttpPut("{id}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Edit(string id, [FromBody] ProjectTypeDto projectType)
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
  }
}