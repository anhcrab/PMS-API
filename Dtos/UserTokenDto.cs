namespace api.Dtos
{
  public class UserTokenDto
  {
    public string? Token { get; set; }
    public DateTime? Expiration { get; set; }
  }
}