using api.Helpers;

namespace api.Interfaces
{
  public interface ISendMailService
  {
    Task SendMail(MailContent mailContent);
    Task SendEmailAsync(string email, string subject, string htmlMessage);
  }
}