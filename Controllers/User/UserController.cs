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
    public async Task<IActionResult> ListUsers()
    {
      return Ok(await _service.AllAsync());
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
  }
}