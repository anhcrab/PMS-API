using api.Dtos;

namespace api.Interfaces
{
  public interface IProfileService
  {
    Task<List<ProfileExtDto>> All();
  }
}