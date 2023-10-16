using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MySite.Models;
using MySite.Services.EmailSender;
using MySite.Services.EmailSender.Model;
using System.Diagnostics;

namespace MySite.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        private readonly IEmailSender _emailsender;


        public HomeController(ILogger<HomeController> logger, IEmailSender emailsender)
        {
            _logger = logger;
            _emailsender = emailsender;
        }

        

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }


       
        [HttpPost]
        public async Task<IActionResult> SendMail(SendEmailVM EmailVM)
        {
            try
            {
                var EmailV = EmailVM.Email;
                //var toEmail = "dogbanx@gmail.com";                
                var subject = "You have successfully subscribed to my site's newsletter";
                var message = "Hello, you have successfully subscribed to my site's newsletter";
                _emailsender.SendEmail(EmailV, subject, message).GetAwaiter();

                TempData["SuccessMessage"] = "You have successfully subscribed to my site's newsletter...";

                return RedirectToAction("Index");

            }
            catch(Exception ex)
            {
                throw;
            }

        }
    }
}