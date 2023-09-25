
namespace MySite.Services
{
    public interface IEmailSender
    {
        Task SendEmail(string toEmail, string subject, string message);
    }
}
