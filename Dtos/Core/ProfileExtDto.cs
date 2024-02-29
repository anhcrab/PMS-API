using api.Dtos.Core;

namespace api.Dtos
{
  public class ProfileExtDto : ProfileDto
  {
    public string Id { get; set; } = null!;
    public string UserName { get; set; } = null!;
    public string Email { get; set; } = null!;
  }
}