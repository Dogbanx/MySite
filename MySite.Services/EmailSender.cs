using MimeKit;
using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Configuration;

namespace MySite.Services
{
    public class EmailSender:IEmailSender
    {
        public readonly IConfiguration _configuration;

        public EmailSender(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task SendEmail(string toEmail, string subject, string message)
        {
            try
            {
                var smtpServer = _configuration["EmailSettings:SmtpServer"];
                //var smtpPort = int.Parse(s: _configuration["EmailSettings:SmtpPort"]);
                var smtpEmail = _configuration["EmailSettings:SmtpEmail"];
                var smtpPassword = _configuration["EmailSettings:SmtpPassword"];

                var emailMessage = new MimeMessage();
                emailMessage.From.Add(new MailboxAddress("Your Name", smtpEmail));
                emailMessage.To.Add(new MailboxAddress("", toEmail));
                emailMessage.Subject = subject;

                var bodyBuilder = new BodyBuilder();
                bodyBuilder.HtmlBody = message;

                emailMessage.Body = bodyBuilder.ToMessageBody();

                using (var client = new SmtpClient())
                {

                    await client.ConnectAsync(smtpServer, 587);
                    await client.AuthenticateAsync(smtpEmail, smtpPassword);
                    await client.SendAsync(emailMessage);
                    await client.DisconnectAsync(true);
                }

            }
            catch (Exception)
            {
                // Обробляйте помилки відправки email
                throw;
            }
        }
    }
}