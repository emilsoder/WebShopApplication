using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebShopApp.Models;
using WebShopApp.Repositories;
using Microsoft.AspNetCore.Authorization;

namespace WebShopApp.Controllers
{
    public class ProductsController : Controller
    {
        #region props
        public static string _selectedCategory { get; set; }
        private string GetUserName
        {
            get
            {
                return User.Identity.Name;
            }
        }
        #endregion

        private IProductRepository _productRepository;
        private IShoppingCartDetailsRepository _shoppingCartDetailsRepository;
        private IShoppingCartRepository _shoppingCartRepository;
        private ICustomerRepository _customerRepository;
        private ApplicationDbContext _context;
        public ProductsController(ApplicationDbContext ctx, IProductRepository productRepository,
            IShoppingCartDetailsRepository shoppingCartDetailsRepository,
            IShoppingCartRepository shoppingCartRepository,
            ICustomerRepository customerRepository)
        {
            _context = ctx;
            #region init
            this._productRepository = new ProductRepository(_context);
            this._shoppingCartDetailsRepository = new ShoppingCartDetailsRepository(_context);
            #endregion
            this._shoppingCartRepository = shoppingCartRepository;
            this._customerRepository = customerRepository;
        }

        public async Task<IActionResult> Index(string selectedCategory)
        {
            ViewData["selectedCategory"] = _selectedCategory ?? "pizza";
            var products = await _productRepository.GetProducts();
            return View(products.ToList());
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            _shoppingCartDetailsRepository.RemoveItemFromCart(id);
            return RedirectToAction("Index");
        }

        [HttpPost, ActionName("AddProduct")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddProduct(int productID, string selectedCategory)
        {
            _selectedCategory = selectedCategory;

            if (User.Identity.IsAuthenticated)
            {
                var customerID = _customerRepository.GetCustomerID(GetUserName);
                if (!await _shoppingCartRepository.ShoppingCartExists(customerID))
                {
                    _shoppingCartRepository.InsertShoppingCart(customerID);
                    //_shoppingCartRepository.Save();
                }
                else
                {
                    var product = await _productRepository.GetProductByID(productID);
                    _context.ShoppingCartDetails.Add(new ShoppingCartDetails
                    {
                        ShoppingCartID = customerID,
                        ProductID = product.ProductID,
                        UnitPrice = product.UnitPrice,
                        ProductName = product.ProductName,
                        Quantity = 1
                    });
                    _context.SaveChanges();

                    return RedirectToAction(nameof(ProductsController.Index), "Products");
                }
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }
            return RedirectToAction(nameof(ProductsController.Index), "Products");
        }
    }
}
