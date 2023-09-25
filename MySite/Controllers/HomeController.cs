﻿using Microsoft.AspNetCore.Mvc;
using MySite.Models;
using MySite.Services;
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



        public async Task<IActionResult> Index()
        {
            var toEmail = "lelah.okuneva92@ethereal.email";
            var subject = "Test3";
            var message = "Hello";
            await _emailsender.SendEmail(toEmail, subject, message);

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
    }
}