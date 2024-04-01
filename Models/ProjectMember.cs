using System.ComponentModel.DataAnnotations;

namespace api.Models
{
  public class ProjectMember
  {
    [Key]
    public string Id { get; set; } = Guid.NewGuid().ToString();
    public string ProjectId { get; set; } = null!;
    public string MemberId { get; set; } = null!;
    public Project? Project { get; set; }
    public AppUser? Member { get; set; }
  }
}