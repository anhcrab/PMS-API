using api.Dtos.Core;
using api.Extensions;
using api.Interfaces.Core;
using api.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers.Profile
{
  [ApiController]
  [Route("api/profile")]
  [Authorize]
  public class ProfileController(UserManager<AppUser> userManager, IProfileService profileService, RoleManager<IdentityRole> roleManager) : ControllerBase
  {
    private readonly UserManager<AppUser> _userManager = userManager;
    private readonly RoleManager<IdentityRole> _roleManager = roleManager;
    private readonly IProfileService _service = profileService;

    [HttpGet]
    public async Task<IActionResult> PersonalProfile()
    {
      var user = await _userManager.FindByEmailAsync(User.GetEmail());
      return Ok(user);
    }

    [HttpPost]
    public async Task<IActionResult> UpdateProfile([FromBody] ProfileDto profileDto)
    {
      if (!ModelState.IsValid) return BadRequest(ModelState);
      var user = await _userManager.FindByEmailAsync(User.GetEmail());
      if (user == null) return BadRequest("Invalid User Profile");
      user.FirstName = profileDto.FirstName;
      user.LastName = profileDto.LastName;
      user.PhoneNumber = profileDto.PhoneNumber;
      user.Dob = profileDto.Dob;
      user.Sex = profileDto.Sex;
      user.Description = profileDto.Description ??= "";
      user.Address = profileDto.Address ??= "";
      var res = await _userManager.UpdateAsync(user);
      return res.Succeeded ? NoContent() : Ok(res.Errors);
    }

    [HttpPost("generate")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Generate([FromBody] NewUserDto newUserDto)
    {
      if (!ModelState.IsValid) return BadRequest(ModelState);
      var newUser = new AppUser
      {
        UserName = newUserDto.UserName,
        Email = newUserDto.Email
      };
      var result = await _userManager.CreateAsync(newUser, newUserDto.Password);
      if (!result.Succeeded) return StatusCode(500, result.Errors);
      return NoContent();
    }

    [HttpPost("upload")]
    public async Task<IActionResult> Upload(IFormFile file)
    {
      try
      {
        var result = await WriteFile(file);
        return Ok(result);
      }
      catch (Exception)
      {
        throw;
      }
    }

    private static async Task<string> WriteFile(IFormFile file)
    {
      try
      {
        var execpath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory + "terus-content", file.FileName);
        using (var stream = new FileStream(execpath, FileMode.Create))
        {
          await file.CopyToAsync(stream);
        }
        return file.FileName;
      }
      catch (Exception)
      {
        throw;
      }
    }
  }
}