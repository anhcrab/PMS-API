namespace api.Dtos.Projects
{
  public class ProjectTypeDto
  {
    public string? Id { get; set; }
    public string? Name { get; set; }
    public string? AdditionalInfo { get; set; }
    public ICollection<ProjectDto> Projects { get; set; } = [];
  }
}