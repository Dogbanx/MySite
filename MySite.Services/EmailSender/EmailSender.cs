using MimeKit;
using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Configuration;

//using System.Net;
//using System.Net.Mail;

namespace MySite.Services.EmailSender
{
    public class EmailSender : IEmailSender
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
                //var smtpServer = _configuration["EmailSettings:SmtpServer"];
                //var smtpPort = int.Parse(s: _configuration["EmailSettings:SmtpPort"]);
                //var smtpEmail = _configuration["EmailSettings:SmtpEmail"];
                //var smtpPassword = _configuration["EmailSettings:SmtpPassword"];

                //var emailMessage = new MimeMessage();
                //emailMessage.Sender = MailboxAddress.Parse(smtpEmail);
                //emailMessage.To.Add(MailboxAddress.Parse(toEmail));
                //emailMessage.Subject = subject;

                //var builder = new BodyBuilder();
                //builder.HtmlBody = message;
                //emailMessage.Body = builder.ToMessageBody();

                //using var smth = new SmtpClient();
                //smth.Connect(smtpServer, smtpPort, SecureSocketOptions.StartTls);
                //smth.Authenticate(smtpEmail, smtpPassword);
                //await smth.SendAsync(emailMessage);
                //smth.Disconnect(true);


                //1


                var smtpServer = _configuration["EmailSettings:SmtpServer"];
                //var smtpPort = int.Parse(s: _configuration["EmailSettings:SmtpPort"]);
                var smtpEmail = _configuration["EmailSettings:SmtpEmail"];
                var smtpPassword = _configuration["EmailSettings:SmtpPassword"];

                var emailMessage = new MimeMessage();

                emailMessage.From.Add(new MailboxAddress("Your Name", smtpEmail));
                emailMessage.To.Add(new MailboxAddress("1", toEmail));
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



                //2

                //MailAddress from = new MailAddress("mazurbogdan88@gmail.com", "Your Name");
                //MailAddress to = new MailAddress("dogbanx@gmail.com");
                //MailMessage m = new MailMessage(from, to);
                //m.Subject = "Тест";
                //m.Body = "Тест";
                //SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587);
                //smtp.Credentials = new NetworkCredential("mazurbogdan88@gmail.com", "jewd svxj fwns hpmy");
                //smtp.EnableSsl = true;
                //await smtp.SendMailAsync(m);

            }
            catch (Exception ex)
            {
                // Обробляйте помилки відправки email
                throw ex;
            }
        }
    }
}