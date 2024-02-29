using api.Dtos.Core;
using api.Extensions;
using api.Helpers;
using api.Interfaces.Core;
using api.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace api.Controllers.Auth
{
  [ApiController]
  [Route("api/auth")]
  public class AuthController(
    UserManager<AppUser> userManager,
    IConfiguration config,
    ITokenService tokenService,
    SignInManager<AppUser> signInManager,
    ISendMailService mailService
) : ControllerBase
  {
    private readonly UserManager<AppUser> _userManager = userManager;
    private readonly IConfiguration _config = config;
    private readonly ITokenService _tokenService = tokenService;
    private readonly SignInManager<AppUser> _signInManager = signInManager;
    private readonly ISendMailService _mailService = mailService;

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterDto registerDto)
    {
      if (!ModelState.IsValid)
      {
        return BadRequest(ModelState);
      }
      var appUser = new AppUser
      {
        UserName = registerDto.Username,
        Email = registerDto.Email,
      };
      var createdUser = await _userManager.CreateAsync(appUser, registerDto.Password!);
      if (createdUser.Succeeded)
      {
        var roleResult = await _userManager.AddToRolesAsync(
          appUser,
          appUser.Email == _config["AdminEmail"] ? ["ADMIN", "MANAGER", "EMPLOYEE"] : ["CLIENT"]
        );
        if (roleResult.Succeeded)
        {
          if (appUser.Email == _config["AdminEmail"])
          {
            appUser.Department = Departments.ADMINISTRATION;
            appUser.Position = "Quản trị viên";
            appUser.Status = EmployeeStatuses.EMPLOYED;
            await _userManager.UpdateAsync(appUser);
          }
          return Ok(await _tokenService.CreateToken(appUser));
        }
        else
        {
          return StatusCode(500, roleResult.Errors);
        }
      }
      else
      {
        return StatusCode(500, createdUser.Errors);
      }
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginDto loginDto)
    {
      try
      {
        if (!ModelState.IsValid)
        {
          return BadRequest(ModelState);
        }
        var user = await _userManager.Users.FirstOrDefaultAsync(x => x.Email == loginDto.Email);
        if (user == null)
        {
          return Unauthorized("Invalid Email");
        }
        var result = await _signInManager.CheckPasswordSignInAsync(user, loginDto.Password!, false);
        if (!result.Succeeded)
        {
          return Unauthorized("Email not found and/or password not correct");
        }
        return Ok(await _tokenService.CreateToken(user));
      }
      catch (Exception e)
      {
        return StatusCode(500, e);
      }
    }
    [HttpGet("testmail")]
    public async Task<IActionResult> TestMail()
    {
      MailContent content = new()
      {
        To = "anhdqse182384@fpt.edu.vn",
        Subject = "Kiểm tra thử",
        Body = "<p><strong>Xin chào bạn!</strong><br>Đây là mail test.</p>"
      };
      await _mailService.SendMail(content);
      return Ok("Send mail");
    }

    [HttpPost("forgot")]
    public async Task<IActionResult> ForgotPassword([FromBody] ForgotDto forgotDto)
    {
      if (!ModelState.IsValid) return BadRequest("Invalid Email");
      var user = await _userManager.Users.FirstOrDefaultAsync(u => u.Email == forgotDto.EmailAddress);
      if (user == null) return NotFound("Email not found");
      var content = new MailContent
      {
        To = forgotDto.EmailAddress,
        Subject = "Lấy lại mật khẩu",
        Body = MailTemplate.ResetPassword()
      };
      await _mailService.SendMail(content);
      return Ok("Success");
    }

    [HttpGet]
    [Route("role")]
    [Authorize]
    public IActionResult Role()
    {
      return Ok(User.GetRole());
    }

    [HttpGet]
    [Route("roles")]
    [Authorize]
    public IActionResult Roles()
    {
      return Ok(User.GetRoles());
    }

    [HttpGet("test")]
    public IActionResult Test()
    {
      return Ok("sdfsd");
    }
  }
}