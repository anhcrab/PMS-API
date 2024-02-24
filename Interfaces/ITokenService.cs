using api.Dtos;
using api.Models;

namespace api.Interfaces
{
  public interface ITokenService
  {
    Task<UserTokenDto> CreateToken(AppUser user);
  }
}