using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebShopApp.Models;
using WebShopApp.Repositories;
using WebShopApp.Models.AdministratorViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;

namespace WebShopApp.Controllers
{
    [Authorize(Roles = "Administrator")]
    public class AdministratorController : Controller
    {
        private IProductRepository _productRepository;
        private ICustomerRepository _customerRepository;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ApplicationDbContext _context;

        public AdministratorController(ICustomerRepository customerRepository, IProductRepository productRepository, UserManager<ApplicationUser> userManager, ApplicationDbContext context)
        {
            _context = context;
            _userManager = userManager;

            _customerRepository = customerRepository;
            _productRepository = productRepository;
        }

        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> Index(int id, string showModal)
        {

            AdministratorViewModel adminViewModel = new AdministratorViewModel
            {
                AllCustomers = await _customerRepository.GetCustomers(),
                AllProducts = await _productRepository.GetProducts(),
                orders = await _context.Orders.ToListAsync()
            };

            ViewBag.Categories = new SelectList(await _productRepository.AllCategories());

            ViewBag.showModal = showModal ?? null;
            ViewBag.productID = id;
            ViewBag.customerID = id;

            return View(adminViewModel);
        }

        [Authorize(Roles = "Administrator")]
        [HttpPost, ActionName("EditSelectedProduct")]
        public IActionResult EditSelectedProduct(int id, AdministratorViewModel product)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var catName = product.OneProductVM.CategoryName.ToLower().Trim();

                    product.OneProduct.CategoryNumber = (catName == "pizza") ? 1 : (catName == "kebab") ? 2 : (catName == "sallad") ? 3 : 0;
                    _productRepository.UpdateProduct(product.OneProduct);
                }
                catch (DbUpdateException)
                {
                    return RedirectToAction("Index", routeValues: null);
                }
            }
            return RedirectToAction("Index", routeValues: null);
        }

        [Authorize(Roles = "Administrator")]
        [HttpPost, ActionName("EditSelectedCustomer")]
        public async Task<IActionResult> EditSelectedCustomer(int id, AdministratorViewModel customer)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    ApplicationUser _user = await _userManager.FindByEmailAsync(customer.OneCustomer.Email);
                    if (_user != null)
                    {
                        // Get the users roles as IList<string>
                        var currentRoles = await _userManager.GetRolesAsync(_user);

                        // Remove the users current role(s)
                        var removeRolesOperation = await _userManager.RemoveFromRolesAsync(user: _user, roles: currentRoles);

                        // Add to selected role
                        // if previous operation was excecuted successfully
                        if (removeRolesOperation.Succeeded)
                        {
                            var _role = await _userManager.AddToRoleAsync(_user, role: customer.OneCustomer.Role);
                        }

                        // Update the customer in Customers model
                        _customerRepository.UpdateCustomer(customer.OneCustomer);

                        return RedirectToAction("Index", routeValues: null);
                    }
                }
                catch (DbUpdateException)
                {
                    return RedirectToAction("Index", routeValues: null);
                }
            }
            return View("Index", customer);
            //return RedirectToAction("Index", routeValues: null);
        }


        [Authorize(Roles = "Administrator")]
        [HttpPost, ActionName("DeleteSelectedProduct")]
        public IActionResult DeleteSelectedProduct(int productID)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _productRepository.DeleteProduct(productID);
                }
                catch (DbUpdateException)
                {
                    return RedirectToAction("Index", routeValues: null);
                }
            }
            return RedirectToAction("Index", routeValues: null);
        }

        [Authorize(Roles = "Administrator")]
        [HttpPost, ActionName("CreateProduct")]
        public async Task<IActionResult> CreateProduct(AdministratorViewModel product)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var catName = product.OneProductVM.CategoryName.ToLower().Trim();
                    int categoryNumber = (catName == "pizza") ? 1 : (catName == "kebab") ? 2 : (catName == "sallad") ? 3 : 0;

                    _productRepository.InsertProduct(new Products
                    {
                        ProductName = product.CreateProduct.ProductName,
                        CategoryNumber = categoryNumber,
                        Description = product.CreateProduct.Description,
                        UnitPrice = product.CreateProduct.UnitPrice
                    });
                    return RedirectToAction("Index", routeValues: null);
                }
                catch (DbUpdateException)
                {
                    return RedirectToAction("Index", routeValues: null);
                }
            }
            return RedirectToAction("Index");
        }
    }
}