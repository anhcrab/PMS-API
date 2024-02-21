using api.Dtos;
using api.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers.Departments
{
  [ApiController]
  [Route("api/departments")]
  [Authorize(Roles = "Admin")]
  public class DepartmentController(IDepartmentService service) : ControllerBase
  {
    private readonly IDepartmentService _service = service;

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
      return Ok(await _service.GetAllAsync());
    }
    [HttpGet("{id}")]
    public async Task<IActionResult> Get(string id)
    {
      return Ok(await _service.GetByIdAsync(id));
    }
    [HttpPost]
    public async Task<IActionResult> Add([FromBody] NewDepartmentDto departmentDto)
    {
      if (!ModelState.IsValid) return BadRequest(ModelState);
      await _service.CreateAsync(departmentDto);
      return NoContent();
    }
    [HttpPut("{id}")]
    public async Task<IActionResult> Update(string id, [FromBody] DepartmentDto departmentDto)
    {
      if (!ModelState.IsValid) return BadRequest(ModelState);
      var department = await _service.GetByIdAsync(id);
      if (department == null) return BadRequest("Department not exists");
      await _service.UpdateAsync(id, departmentDto);
      return NoContent();
    }
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(string id)
    {
      var department = await _service.GetByIdAsync(id);
      if (department == null) return BadRequest("Target does not exists");
      await _service.DeleteAsync(id);
      return NoContent();
    }
  }
}