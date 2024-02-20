using Microsoft.AspNetCore.Identity;

namespace api.Models
{
  public class AppUser : IdentityUser
  {
    public string Description { get; set; } = string.Empty;
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string Sex { get; set; } = "male";
    public string Dob { get; set; } = string.Empty;
    public string AdditionalInfo { get; set; } = string.Empty;
  }
}