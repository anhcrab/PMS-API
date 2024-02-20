using api.Dtos;
using api.Extensions;
using api.Helpers;
using api.Interfaces;
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
    ISendMailService mailService,
    IEmployeeService employeeService
) : ControllerBase
  {
    private readonly UserManager<AppUser> _userManager = userManager;
    private readonly IConfiguration _config = config;
    private readonly ITokenService _tokenService = tokenService;
    private readonly SignInManager<AppUser> _signInManager = signInManager;
    private readonly ISendMailService _mailService = mailService;
    private readonly IEmployeeService _employeeService = employeeService;

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterDto registerDto)
    {
      try
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
          var roleResult = await _userManager.AddToRoleAsync(appUser, appUser.Email == _config["AdminEmail"] ? "ADMIN" : "CLIENT");
          if (roleResult.Succeeded)
          {
            return Ok(new UserTokenDto
            {
              Token = await _tokenService.CreateToken(appUser)
            });
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
      catch (Exception e)
      {
        return StatusCode(404, e);
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
        return Ok(new UserTokenDto
        {
          Token = await _tokenService.CreateToken(user)
        });
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
    public async Task<IActionResult> Role()
    {
      return Ok(await _employeeService.GetRole(User.GetEmail()));
    }

    [HttpGet("test")]
    public IActionResult Test()
    {
      return Ok("sdfsd");
    }
  }
}