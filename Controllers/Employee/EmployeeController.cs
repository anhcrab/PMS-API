using api.Dtos.Employees;
using api.Extensions;
using api.Interfaces.Employees;
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

    [HttpGet]
    [Route("page/{paged}")]
    [Authorize(Roles = "Admin, Manager")]
    public async Task<IActionResult> Paginate(int paged)
    {
      return Ok(await _service.PaginateAsync(paged));
    }

    [HttpPost]
    [Authorize(Roles = "Admin, Manager")]
    public async Task<IActionResult> Add([FromBody] NewEmployee newEmployee)
    {
      var res = await _service.CreateAsync(newEmployee.Id, newEmployee.Department, newEmployee.SupervisorId);
      return Ok(res != null ? res : "null mẹ rồi");
    }

    [HttpDelete("{id}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Delete(string id)
    {
      await _service.DeleteAsync(id);
      return NoContent();
    }

    [HttpGet("promotion/{id}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Promote(string id)
    {
      await _service.PromoteAsync(id);
      return NoContent();
    }

    [HttpGet("demotion/{id}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Demote(string id)
    {
      await _service.DemoteAsync(id);
      return NoContent();
    }

    [HttpGet("room")]
    [Authorize(Roles = "Admin, Manager")]
    public async Task<IActionResult> Room()
    {
      return Ok(await _service.Room(User.GetEmail()));
    }

    [HttpGet("myteam/")]
    public async Task<IActionResult> MyTeam()
    {
      return Ok(await _service.Team(User.GetEmail()));
    }
  }
}