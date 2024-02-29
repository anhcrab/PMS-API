using api.Dtos;
using api.Dtos.Core;
using api.Models;

namespace api.Interfaces.Core
{
  public interface ITokenService
  {
    Task<UserTokenDto> CreateToken(AppUser user);
  }
}