using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using api.Interfaces;
using api.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;

namespace api.Services
{
  public class TokenService : ITokenService
  {
    private readonly IConfiguration _config;
    private readonly SymmetricSecurityKey _key;
    private readonly UserManager<AppUser> _userManager;

    public TokenService(IConfiguration config, UserManager<AppUser> userManager)
    {
      _config = config;
      _key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(_config["JWT:Secret"]!));
      _userManager = userManager;
    }

    public async Task<string> CreateToken(AppUser user)
    {
      var _opt = new IdentityOptions();
      var role = await _userManager.GetRolesAsync(user);
      var claims = new List<Claim>
      {
        new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
        new(JwtRegisteredClaimNames.Email, user.Email!),
        new(JwtRegisteredClaimNames.Sub, user.Email!),
        new(_opt.ClaimsIdentity.RoleClaimType, role.First()),
        new(_opt.ClaimsIdentity.UserIdClaimType, user.Id),
        new(_opt.ClaimsIdentity.UserNameClaimType, user.UserName!),
      };

      var credentials = new SigningCredentials(_key, SecurityAlgorithms.HmacSha256);
      var tokenDescriptor = new SecurityTokenDescriptor
      {
        Subject = new ClaimsIdentity(claims),
        Expires = DateTime.Now.AddDays(7),
        SigningCredentials = credentials,
        Issuer = _config["JWT:ValidIssuer"],
        Audience = _config["JWT:ValidAudience"]
      };
      var tokenHandler = new JwtSecurityTokenHandler();
      var token = tokenHandler.CreateToken(tokenDescriptor);
      return tokenHandler.WriteToken(token);
    }
  }
}