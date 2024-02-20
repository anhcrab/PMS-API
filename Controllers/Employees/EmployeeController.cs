using api.Dtos;
using api.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers.Employees
{
  [ApiController]
  [Route("api/employees")]
  [Authorize]
  public class EmployeeController(IEmployeeService service) : ControllerBase
  {
    private readonly IEmployeeService _service = service;
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
      return Ok(await _service.All());
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(string id)
    {
      return Ok(await _service.FindById(id));
    }
    [HttpPost]
    public async Task<IActionResult> AddNew([FromBody] RegisterEmployeeDto employeeDto)
    {
      if (!ModelState.IsValid) return BadRequest(ModelState);
      var employee = await _service.Register(employeeDto.UserId, employeeDto.SupervisorId, employeeDto.DepartmentId);
      return Ok(employee);
    }
    [HttpPut("{id}")]
    public async Task<IActionResult> Update(string id, [FromBody] UpdateEmployeeDto employeeDto)
    {
      var employee = await _service.FindById(id);
      if (employee == null) return BadRequest("Not exists");
      await _service.Update(id, employeeDto);
      return NoContent();
    }
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(string id)
    {
      await _service.Delete(id);
      return NoContent();
    }
  }
}