namespace api.Dtos.Projects
{
  public class UpdateProjectDto
  {
    public string? Name { get; set; }
    public string? ResponsibleId { get; set; } 
    public string? Progress { get; set; }
    public string? TypeId { get; set; }
    public double Budget { get; set; }
    public string? Deadline { get; set; }
    public string? PaymentDate { get; set; }
    public string? AdditionalInfo { get; set; }
    public string? Status { get; set; }
    public List<string> Members { get; set; } = [];
  }
}