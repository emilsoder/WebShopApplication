using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebShopApp.Models;
using WebShopApp.Repositories;
using WebShopApp.Services;
using System;
using System.Collections.Generic;

namespace WebShopApp.Controllers
{
    public class ShoppingCartController : Controller
    {
        #region props
        public static string OrderSent = null;
        public string breakline = "\n--------------------------------------\n";
        #endregion

        private ApplicationDbContext _context;
        private IShoppingCartDetailsRepository _shoppingCartDetailsRepository;
        private ICustomerRepository _customerRepository;
        private IEmailSender _emailSender;

        public ShoppingCartController(ApplicationDbContext context,
            IEmailSender emailSender,
            IShoppingCartDetailsRepository shoppingCartDetailsRepository,
            IShoppingCartRepository shoppingCartRepository,
            ICustomerRepository customerRepository)
        {
            this._context = context;
            this._shoppingCartDetailsRepository = shoppingCartDetailsRepository;
            this._customerRepository = customerRepository;
            this._emailSender = emailSender;
        }

        public async Task<IActionResult> Index()
        {
            if (!User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Login", "Account");
            }
            var customerID = _customerRepository.GetCustomerID(User.Identity.Name);

            ViewData["OrderSent"] = OrderSent ?? null;
            OrderSent = null;
            return View(_shoppingCartDetailsRepository.ShoppingCartDetailsByIDAsEnumerable(customerID));
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            _shoppingCartDetailsRepository.RemoveItemFromCart(id);
            return RedirectToAction("Index");
        }

        [HttpPost, ActionName("SendOrder")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SendOrder()
        {
            var customerID = _customerRepository.GetCustomerID(User.Identity.Name);
            var customer = await _customerRepository.GetCustomerByID(customerID);
            var shoppingCartDetails = _shoppingCartDetailsRepository.ShoppingCartDetailsByIDAsEnumerable(customerID);

            string message = MessageBuilder(shoppingCartDetails.ToList(), customer);

            await _emailSender.SendEmailAsync(User.Identity.Name, "Order", message, null, false);

            OrderSent = "OrderSent";
            InsertOrder(shoppingCartDetails.ToList(), customer);

            return RedirectToAction("Index", "ShoppingCart");
        }
        private string MessageBuilder(List<ShoppingCartDetails> _shoppingCartDetails, Customers customer)
        {
            string productsString = "";

            _shoppingCartDetails.ForEach(item => productsString += $"{breakline}Nummer: {item.ProductID}\nProdukt: { item.ProductName}\nPris: { item.UnitPrice}");

            string customerString = $"\nKund: {customer.FirstName} {customer.LastName}\nAddress: {customer.Address}\nTelefon: {customer.Phone}\nEmail: {customer.Email}";

            return $"Produkter{productsString} \n\nKunduppgifter{customerString}";
        }

        public void InsertOrder(List<ShoppingCartDetails> _shoppingCart, Customers customer)
        {
            Orders newOrder = new Orders();
            newOrder.CustomerID = customer.CustomerID;
            newOrder.NetPrice = _shoppingCart.Sum(x => x.UnitPrice);
            newOrder.ProductsQty = _shoppingCart.Count();
            newOrder.TotalPrice = (User.IsInRole("PremiumUser")) ? (80m / 100m) * newOrder.NetPrice : newOrder.NetPrice;
            newOrder.OrderDate = DateTime.Now;
            newOrder.Delivery = "Avhämtning";
            newOrder.Discount = (User.IsInRole("PremiumUser")) ? 20 : 0;
            _context.Orders.Add(newOrder);
            _context.SaveChanges();
        }
    }
}
