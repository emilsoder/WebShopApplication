using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebShopApp.Services;
using WebShopApp.Models.ViewModels;

namespace WebShopApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly IEmailSender _emailSender;
        public HomeController(IEmailSender emailSender)
        {
            _emailSender = emailSender;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact(string IsMessageSent)
        {
            ViewData["IsMessageSent"] = IsMessageSent;

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Index(EmailFormViewModel model)
        {
            if (ModelState.IsValid)
            {
                await _emailSender.SendEmailAsync(model.FromEmail, model.Subject, model.Message, model.FromName, false);
                return RedirectToAction("Contact", new { IsMessageSent = "OK" });
            }
            return View(model);
        }

        public IActionResult Error()
        {
            return View();
        }
    }
}
