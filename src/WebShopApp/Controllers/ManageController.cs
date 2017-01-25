using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using WebShopApp.Models;
using WebShopApp.Services;
using WebShopApp.Repositories;
using Microsoft.EntityFrameworkCore;
using WebShopApp.Models.ViewModels.ManageViewModels;

namespace WebShopApp.Controllers
{
    [Authorize]
    public class ManageController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly ICustomerRepository _customerRepository;

        public ManageController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, ApplicationDbContext dbContext, ICustomerRepository customerRepository)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _customerRepository = customerRepository;
        }

        [HttpGet]
        public async Task<IActionResult> Index(string OrderSent, ManageMessageId? message = null)
        {
            ViewData["StatusMessage"] = StatusMessageHelper(message);

            var user = await GetCurrentUserAsync();
            if (user == null)
            {
                return View("Error");
            }

            var customer = await _customerRepository.GetCustomerByID(_customerRepository.GetCustomerID(User.Identity.Name));

            var customerViewModel = new Customers
            {
                CustomerID = customer.CustomerID,
                Address = customer.Address,
                Email = customer.Email,
                FirstName = customer.FirstName,
                LastName = customer.LastName,
                Phone = customer.Phone
            };

            var viewModel = new ManageViewModel
            {
                customer = customerViewModel,
                changePasswordViewModel = new ChangePasswordViewModel()
            };

            ViewData["OrderSent"] = OrderSent ?? null;
            return View(viewModel);
        }
        
        [HttpPost, ActionName("UpdateCustomerDetails")]
        [ValidateAntiForgeryToken]
        public IActionResult UpdateCustomerDetails([Bind("CustomerID,FirstName,LastName,Address,Phone,Email")] ManageViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _customerRepository.EditCustomerDetails(model.customer);
                    return RedirectToAction("Index", new { Message = ManageMessageId.UpdateDetailsSuccess });
                }
                catch (DbUpdateConcurrencyException)
                {
                    return RedirectToAction("Index");
                }
            }
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult ChangePassword()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ChangePassword(ManageViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model.changePasswordViewModel);
            }
            var user = await GetCurrentUserAsync();
            if (user != null)
            {
                var result = await _userManager.ChangePasswordAsync(user, model.changePasswordViewModel.OldPassword, model.changePasswordViewModel.NewPassword);
                if (result.Succeeded)
                {
                    await _signInManager.SignInAsync(user, isPersistent: false);
                    return RedirectToAction(nameof(Index), new { Message = ManageMessageId.ChangePasswordSuccess });
                }
                AddErrors(result);
                return View(model);
            }
            return RedirectToAction(nameof(Index), new { Message = ManageMessageId.Error });
        }

        #region Helpers
        public string StatusMessageHelper(ManageMessageId? message)
        {
            string statusMessage = message == ManageMessageId.ChangePasswordSuccess ? "Ditt lösenord har ändrats." : message == ManageMessageId.UpdateDetailsSuccess ? "Dina uppgifter har ändrats." : message == ManageMessageId.Error ? "Ett fel har inträffat." : "";
            return statusMessage;
        }

        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }
        }

        public enum ManageMessageId
        {
            ChangePasswordSuccess,
            UpdateDetailsSuccess,
            Error
        }

        private Task<ApplicationUser> GetCurrentUserAsync()
        {
            return _userManager.GetUserAsync(HttpContext.User);
        }

        #endregion
    }
}