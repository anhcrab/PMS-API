using System.Security.Claims;

namespace api.Extensions
{
  public static class ClaimsExtensions
  {
    public static string GetEmail(this ClaimsPrincipal user)
    {
      return user.Claims.SingleOrDefault(x => x.Type.Equals(ClaimTypes.Email))!.Value;
    }

    public static string GetRole(this ClaimsPrincipal user)
    {
      var roles = user.GetRoles();
      var role = "Client";
      roles.ForEach(r => {
        if (r == "Admin") role = "Admin";
        if (r == "Manager") {
          if (role != "Admin") role = "Manager";
        }
        if (r == "Employee" && role == "Client") role = "Employee";
      });
      return role;
    }

    public static List<string> GetRoles(this ClaimsPrincipal user)
    {
      var roles = user.Claims.Where(x => x.Type.Equals(ClaimTypes.Role));
      return roles.Select(role => role.Value).ToList();
    }
  }
}