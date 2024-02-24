
namespace api.Dtos
{
  public class PaginateEmployeeDto
  {
    public int TotalPages { get; set; } 
    public int TotalItems { get; set; }
    public List<UserDto>? Items { get; set; }
  }
}