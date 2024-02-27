using System.ComponentModel.DataAnnotations;
using api.Helpers;

namespace api.Models
{
  public class Project
  {
    [Key]
    public string Id { get; set; } = Guid.NewGuid().ToString();
    public string Name { get; set; } = "";
    public string ResponsibleId { get; set; } = ""; 
    public string Progress { get; set; } = "";
    public string TypeId { get; set; } = "";
    public double Budget { get; set; }
    public string Deadline { get; set; } = "";
    public string PaymentDate { get; set; } = "";
    public string AdditionalInfo { get; set; } = "";
    public ProjectStatuses Status { get; set; } = ProjectStatuses.PENDING;
    public AppUser? Responsible { get; set; }
    public ProjectType? Type { get; set; }
    public DateTime Created { get; set; } = DateTime.Now;
    public bool Trash { get; set; } = false;
  }
}