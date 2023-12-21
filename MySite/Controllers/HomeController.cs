using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using MySite.Models;
using MySite.Services.EmailSender;
using MySite.Services.EmailSender.Model;
using Serilog;
using System.Diagnostics;

namespace MySite.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        private readonly IEmailSender _emailsender;

        public HomeController(ILogger<HomeController> logger, IEmailSender emailsender )
        {
            _logger = logger;
            _emailsender = emailsender;
           
        }


        public IActionResult Index()
        {
           
            return View();
        }

        public IActionResult UploadFile()
        {
            return View();
        }

        public IActionResult ValidValidator()
        {
            return View(new ModelValid());
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult SubmitForm(ModelValid model)
        {
            if (ModelState.IsValid)
            {
                return RedirectToAction("Success");
            }
            return RedirectToAction("Index");
        }

        public IActionResult Success()
        {
            return RedirectToAction("Index");
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

                Log.Information("Email => {@EmailV}", EmailV);

                return RedirectToAction("Index");

            }
            catch(Exception ex)
            {
                throw;
            }

        }

        public IActionResult ChangeLanguage(string culture)
        {
            Response.Cookies.Append(CookieRequestCultureProvider.DefaultCookieName, CookieRequestCultureProvider.MakeCookieValue(new RequestCulture(culture)), new CookieOptions()
            {
                Expires = DateTimeOffset.UtcNow.AddYears(1)
            });
            return Redirect(Request.Headers["Referer"].ToString());
        }
    }
}