using System.ComponentModel.DataAnnotations;

namespace api.Dtos.Projects
{
  public class NewProjectTypeDto
  {
    [Required]
    public string Name { get; set; } = "";
    public string AdditionalInfo { get; set; } = "";
  }
}