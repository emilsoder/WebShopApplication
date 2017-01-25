using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WebShopApp.Models;
using WebShopApp.Models.AccountViewModels;
using WebShopApp.Services;
using WebShopApp.Repositories;
using WebShopApp.Models.ViewModels.AccountViewModels;

namespace WebShopApp.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly ApplicationDbContext _context;
        private readonly ICustomerRepository _customerRepository;
        private readonly IShoppingCartRepository _shoppingCartRepository;
        private readonly IEmailSender _emailSender;

        public AccountController(IEmailSender emailSender, UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, ApplicationDbContext context, ICustomerRepository customerRepository, IShoppingCartRepository shoppingCartRepository)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _context = context;
            _customerRepository = customerRepository;
            _shoppingCartRepository = shoppingCartRepository;
            _emailSender = emailSender;
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Login(string returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model, string returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;
            if (ModelState.IsValid)
            {
                var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, lockoutOnFailure: false);
                if (result.Succeeded)
                {
                    return RedirectToLocal(returnUrl);
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Invalid login attempt.");
                    return View(model);
                }
            }
            return View(model);
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Register(string returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterViewModel model, string returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;
            if (ModelState.IsValid)
            {
                var user = new ApplicationUser
                {
                    UserName = model.Email,
                    Email = model.Email
                };

                _context.Customers.Add(new Customers
                {
                    FirstName = model.FirstName,
                    Address = model.Address,
                    LastName = model.LastName,
                    Phone = model.Phone,
                    Email = model.Email,
                    Role = "RegularUser"
                });
                _context.SaveChanges();

                _shoppingCartRepository.InsertShoppingCart(_customerRepository.GetCustomerID(model.Email));

                var result = await _userManager.CreateAsync(user, model.Password);

                if (result.Succeeded)
                {
                    await _userManager.AddToRoleAsync(user, "RegularUser");
                    await _signInManager.SignInAsync(user, isPersistent: false);
                    await _context.SaveChangesAsync();
                    return RedirectToLocal(returnUrl);
                }
                AddErrors(result);
            }
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> LogOff()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Products");
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ForgotPassword(ForgotPasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByNameAsync(model.Email);
                if (user == null)
                {
                    return RedirectToAction("ForgotPassword", new { confirmation = "ForgotPasswordConfirmation" });
                }

                var code = await _userManager.GeneratePasswordResetTokenAsync(user);
                var callbackUrl = Url.Action("ResetPassword", "Account", new { userId = user.Id, code = code }, protocol: HttpContext.Request.Scheme);

                string resetMessage = "Klicka på länken för att återställa ditt lösenord: <a href=\"" + callbackUrl + "\">link</a>";

                await _emailSender.SendEmailAsync(model.Email, "Glömt lösenord", resetMessage, "Tomaso Pizzeria", true);

                return RedirectToAction("ForgotPassword", new { confirmation = "ForgotPasswordConfirmation" });
            }
            return View(model);
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ResetPassword(ResetPasswordViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var user = await _userManager.FindByNameAsync(model.Email);
            if (user == null)
            {
                return RedirectToAction("ForgotPassword", new { confirmation = "ResetConfirmation" });
            }
            var result = await _userManager.ResetPasswordAsync(user, model.Code, model.Password);
            if (result.Succeeded)
            {
                return RedirectToAction("ForgotPassword", new { confirmation = "ResetConfirmation" });
            }
            AddErrors(result);
            return View();
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult ResetPassword(string confirmation, string code = null)
        {
            ViewData["ResetConfirmation"] = (confirmation != null) ? "true" : null;
            return code == null ? View("Error") : View();
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult ForgotPassword(string confirmation)
        {
            ViewData["ForgotPasswordConfirmation"] = (confirmation != null) ? "true" : null;
            return View();
        }

        #region Helpers

        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }
        }

        private IActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            else
            {
                return RedirectToAction(nameof(ProductsController.Index), "Products");
            }
        }

        #endregion
    }
}