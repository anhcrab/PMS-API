using System.ComponentModel.DataAnnotations;

namespace api.Models
{
  public class ProjectType
  {
    [Key]
    public string Id { get; set; } = Guid.NewGuid().ToString();
    public string Name { get; set; } = "";
    public string AdditionalInfo { get; set; } = "";
    public List<Project> Projects { get; set; } = [];
  }
}