using MySite.Services.EmailSender.Model;

namespace MySite.Services.EmailSender
{
    public interface IEmailSender
    {
        Task SendEmail(string toEmail, string subject, string message);
       
    }
}
