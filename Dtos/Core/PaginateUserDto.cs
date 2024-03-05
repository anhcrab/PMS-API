namespace api.Dtos.Core
{
  public class PaginateUserDto
  {
    public int TotalPages { get; set; }
    public int TotalItems { get; set; }
    public List<UserDto>? Items { get; set; }
  }
}