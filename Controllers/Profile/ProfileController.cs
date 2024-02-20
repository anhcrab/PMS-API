using api.Dtos;
using api.Extensions;
using api.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers.Profile
{
  [ApiController]
  [Route("api/profile")]
  [Authorize]
  public class ProfileController(UserManager<AppUser> userManager) : ControllerBase
  {
    private readonly UserManager<AppUser> _userManager = userManager;

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
      user.FirstName = profileDto.firstName;
      user.LastName = profileDto.lastName;
      user.PhoneNumber = profileDto.phoneNumber;
      user.Dob = profileDto.dob;
      user.Sex = profileDto.sex;
      user.Description = profileDto.description ??= "";
      var res = await _userManager.UpdateAsync(user);
      return res.Succeeded ? NoContent() : Ok(res.Errors);
    }
  }
}