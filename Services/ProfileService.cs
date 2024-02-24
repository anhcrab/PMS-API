using api.Dtos;
using api.Interfaces;
using api.Mappers;
using Microsoft.IdentityModel.Tokens;

namespace api.Services
{
  public class ProfileService(IUserRepository repository) : IProfileService
  {
    private readonly IUserRepository _repository = repository;
    public async Task<List<ProfileExtDto>> All()
    {
      var result = await _repository.AllAsync();
      if (result.IsNullOrEmpty()) return [];
      return result.Select(x => x.ToProfileExtDto()).ToList();
    }
  }
}