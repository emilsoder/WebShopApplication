using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebShopApp.Services;
using WebShopApp.Models.ViewModels;

namespace WebShopApp.Controllers
{
    public class ContactController : Controller
    {
        private readonly IEmailSender _emailSender;
        public ContactController(IEmailSender emailSender)
        {
            _emailSender = emailSender;
        }

        public IActionResult Contact(string IsMessageSent)
        {
            ViewData["IsMessageSent"] = IsMessageSent;

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Contact(EmailFormViewModel model)
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
