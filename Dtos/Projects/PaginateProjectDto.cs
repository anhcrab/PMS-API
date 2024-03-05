namespace api.Dtos.Projects
{
  public class PaginateProjectDto
  {
    public int TotalPages { get; set; }
    public int TotalItems { get; set; }
    public List<ProjectDto>? Items { get; set; }
  }
}