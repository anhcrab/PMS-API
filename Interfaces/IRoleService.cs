
using api.Dtos;

namespace api.Interfaces
{
  public interface IRoleService
  {
    Task<List<RoleDto>> AllAsync();
  }
}