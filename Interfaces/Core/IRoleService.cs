using api.Dtos.Core;

namespace api.Interfaces.Core
{
  public interface IRoleService
  {
    Task<List<RoleDto>> AllAsync();
  }
}