namespace api.Models
{
  public class WorkTask
  {
    public string Id { get; set; } = Guid.NewGuid().ToString();
    public string Name { get; set; } = "";
    public string Content { get; set; } = "";
    public bool IsCompleted { get; set; } = false;
    public string ProjectId { get; set; } = null!;
    public string? MemberId { get; set; } = "";
    public Project Project { get; set; } = null!;
    public AppUser? Member { get; set; }
    public DateTime CreationDate { get; set; } = DateTime.Now;
    public DateTime UpdatedDate { get; set; } = DateTime.Now;
    public DateTime? DeletedDate { get; set; }
  }
}