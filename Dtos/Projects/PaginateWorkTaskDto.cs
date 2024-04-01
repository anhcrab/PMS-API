namespace api.Dtos.Projects
{
  public class PaginateWorkTaskDto
  {
    public int TotalPages { get; set; }
    public int TotalItems { get; set; }
    public List<WorkTaskDto>? Items { get; set; }
  }
}