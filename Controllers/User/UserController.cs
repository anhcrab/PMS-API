using api.Dtos.Core;
using api.Interfaces.Core;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers.User
{
  [ApiController]
  [Route("api/users")]
  [Authorize(Roles = "Admin")]
  public class UserController(IUserService service) : ControllerBase
  {
    private readonly IUserService _service = service;

    [HttpGet]
    public async Task<IActionResult> ListUsers(string limit, string page, string? role, string? search)
    {
      return Ok(await _service.PaginateAsync(int.Parse(limit), int.Parse(page), role, search));
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> RetrieveUser(string id)
    {
      return Ok(await _service.ReadAsync(id));
    }

    [HttpPost]
    public async Task<IActionResult> CreateUser([FromBody] NewUserDto newUser)
    {
      await _service.CreateAsync(newUser);
      return NoContent();
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateUser(string id, [FromBody] UserDto updateUser)
    {
      await _service.UpdateAsync(id, updateUser);
      return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteUser(string id)
    {
      await _service.DeleteAsync(id);
      return NoContent();
    }

    [HttpPost("delete")]
    public async Task<IActionResult> DeleteUsers([FromBody]List<string>? ids)
    {
      await _service.MultiDeleteAsync(ids);
      return NoContent();
    }

    [HttpGet("trash")]
    public async Task<IActionResult> TrashView(string limit, string page, string? role, string? search)
    {
      return Ok(await _service.TrashView(int.Parse(limit), int.Parse(page), role, search));
    }

    [HttpPost("trash")]
    public async Task<IActionResult> TrashUsers([FromBody] List<string>? ids)
    {
      await _service.MultiTrashAsync(ids);
      return NoContent();
    }

    [HttpGet("trash/{id}")]
    public async Task<IActionResult> Trash(string id)
    {
      await _service.TrashAsync(id);
      return NoContent();
    }

    [HttpPost("restore")]
    public async Task<IActionResult> RestoreUsers([FromBody] List<string>? ids)
    {
      await _service.MultiRestoreAsync(ids);
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