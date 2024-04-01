using api.Dtos.Core;

namespace api.Dtos.Projects
{
  public class WorkTaskDto
  {
    public string Id { get; set; } = "";
    public string Name { get; set; } = "";
    public string Content { get; set; } = "";
    public bool IsCompleted { get; set; } = false;
    public string Deadline { get; set; } = "";
    public string CreationDate { get; set; } = "";
    public string ProjectId { get; set; } = null!;
    public string? MemberId { get; set; } = "";
    public ProjectDto? Project { get; set; } = null!;
    public UserDto? Member { get; set; }
  }
}