using api.Helpers;
using api.Interfaces;
using MailKit.Security;
using MimeKit;

namespace api.Services
{
  public class SendMailService : ISendMailService
  {
    private readonly IConfiguration _config;
    private readonly ILogger<SendMailService> _logger;
    public SendMailService(IConfiguration config, ILogger<SendMailService> logger)
    {
      _config = config;
      _logger = logger;
      _logger.LogInformation("Create SendMailService");
    }
    public async Task SendEmailAsync(string email, string subject, string htmlMessage)
    {
      await SendMail(new MailContent()
      {
        To = email,
        Subject = subject,
        Body = htmlMessage
      });
    }

    public async Task SendMail(MailContent mailContent)
    {
      var email = new MimeMessage
      {
        Sender = new MailboxAddress(_config["MailSettings:DisplayName"], _config["MailSettings:Mail"])
      };
      email.From.Add(new MailboxAddress(_config["MailSettings:DisplayName"], _config["MailSettings:Mail"]));
      email.To.Add(MailboxAddress.Parse(mailContent.To));
      email.Subject = mailContent.Subject;

      var builder = new BodyBuilder
      {
        HtmlBody = mailContent.Body
      };
      email.Body = builder.ToMessageBody();

      // Dùng SmtpClient của MailKit
      using var smtp = new MailKit.Net.Smtp.SmtpClient();

      try
      {
        smtp.Connect(_config["MailSettings:Host"], Int32.Parse(_config["MailSettings:Port"]!), SecureSocketOptions.StartTls);
        smtp.Authenticate(_config["MailSettings:Mail"], _config["MailSettings:Password"]);
        await smtp.SendAsync(email);
      }
      catch (Exception ex)
      {
        // Gửi mail thất bại, nội dung sẽ được lưu vào thư mục mailssave
        Directory.CreateDirectory("mailssave");
        var emailsavefile = string.Format(@"mailssave/{0}.eml", Guid.NewGuid());
        await email.WriteToAsync(emailsavefile);

        _logger.LogInformation("Lỗi gửi mail, lưu tại - " + emailsavefile);
        _logger.LogError(ex.Message);
      }

      smtp.Disconnect(true);

      _logger.LogInformation("Send mail to " + mailContent.To);
    }
  }
}