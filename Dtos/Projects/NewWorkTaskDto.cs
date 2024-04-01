using System.ComponentModel.DataAnnotations;

namespace api.Dtos.Projects
{
  public class NewWorkTaskDto
  {

    public string Name { get; set; } = "";
    public string Content { get; set; } = "";
    public bool IsCompleted { get; set; } = false;
    [Required]
    public string Deadline { get; set; } = "";
    [Required]
    public string ProjectId { get; set; } = null!;
    [Required]
    public string MemberId { get; set; } = "";
  }
}