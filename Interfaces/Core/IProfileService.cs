using api.Dtos;

namespace api.Interfaces.Core
{
  public interface IProfileService
  {
    Task<List<ProfileExtDto>> All();
  }
}