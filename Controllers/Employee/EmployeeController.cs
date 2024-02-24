using api.Dtos;
using api.Extensions;
using api.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers.Employee
{
  [ApiController]
  [Route("api/employees")]
  [Authorize]
  public class EmployeeController(IEmployeeService service, ILogger<EmployeeController> logger) : ControllerBase
  {
    private readonly IEmployeeService _service = service;

    private readonly ILogger<EmployeeController> _logger = logger;

    [HttpGet]
    [Authorize(Roles = "Admin, Manager")]
    public async Task<IActionResult> List()
    {
      var log = "[" + DateTime.Now.ToString() + "]";
      log += ": " + User.GetEmail();
      log += " - " + "List employees";
      _logger.LogInformation(log);
      return Ok(await _service.AllAsync());
    }

    [HttpGet("/page/{paged}")]
    [Authorize(Roles = "Admin, Manager")]
    public async Task<IActionResult> Paginate(int paged)
    {
      return Ok(await _service.PaginateAsync(paged));
    }

    [HttpPost]
    [Authorize(Roles = "Admin, Manager")]
    public async Task<IActionResult> Add([FromBody] NewEmployee newEmployee)
    {
      // Int32.Parse()
      await _service.CreateAsync(newEmployee.Id, (Helpers.Departments)int.Parse(newEmployee.Department), newEmployee.SupervisorId);
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