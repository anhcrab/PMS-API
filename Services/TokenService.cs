using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using api.Dtos.Core;
using api.Interfaces.Core;
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

    public async Task<UserTokenDto> CreateToken(AppUser user)
    {
      var _opt = new IdentityOptions();
      var roles = await _userManager.GetRolesAsync(user);
      var claims = new List<Claim>
      {
        new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
        new(JwtRegisteredClaimNames.Email, user.Email!),
        new(JwtRegisteredClaimNames.Sub, user.Email!),
        // new(_opt.ClaimsIdentity.RoleClaimType, role),
        new(_opt.ClaimsIdentity.UserIdClaimType, user.Id),
        new(_opt.ClaimsIdentity.UserNameClaimType, user.UserName!),
      };

      roles.ToList().ForEach(role => {
        claims.Add(new(_opt.ClaimsIdentity.RoleClaimType, role));
      });

      var credentials = new SigningCredentials(_key, SecurityAlgorithms.HmacSha256);
      var tokenDescriptor = new SecurityTokenDescriptor
      {
        Subject = new ClaimsIdentity(claims),
        Expires = DateTime.Now.AddDays(1),
        SigningCredentials = credentials,
        Issuer = _config["JWT:ValidIssuer"],
        Audience = _config["JWT:ValidAudience"]
      };
      var tokenHandler = new JwtSecurityTokenHandler();
      var token = tokenHandler.CreateToken(tokenDescriptor);
      return new UserTokenDto
      {
        Token = tokenHandler.WriteToken(token),
        Expiration = token.ValidTo
      };
    }
  }
}