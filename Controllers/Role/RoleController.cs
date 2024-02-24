using api.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers.Role
{
  [ApiController]
  [Route("api/roles")]
  [Authorize(Roles = "Admin")]
  public class RoleController(IRoleService service) : ControllerBase
  {
    private readonly IRoleService _service = service;

    [HttpGet]
    public async Task<IActionResult> ListAsync()
    {
      return Ok(await _service.AllAsync());
    }
  }
}