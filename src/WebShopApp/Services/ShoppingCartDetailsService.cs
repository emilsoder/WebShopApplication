using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebShopApp.Repositories;
using WebShopApp.Models;

namespace WebShopApp.Services
{
    public class ShoppingCartDetailsService : IShoppingCartDetailsService
    {
        private IShoppingCartDetailsRepository _shoppingCartDetailsRepository;
        private ICustomerRepository _customerRepository;

        public ShoppingCartDetailsService(IShoppingCartDetailsRepository shoppingCartDetailsRepository, ICustomerRepository customerRepository)
        {
            this._shoppingCartDetailsRepository = shoppingCartDetailsRepository;
            this._customerRepository = customerRepository;
        }

        public int GetCount(string userName)
        {
            return _shoppingCartDetailsRepository.CountProductsInShoppingCart(_customerRepository.GetCustomerID(userName));
        }

        public List<ShoppingCartDetails> currentShoppingCart(string userName)
        {
            return _shoppingCartDetailsRepository.ShoppingCartDetailsByIDAsEnumerable(_customerRepository.GetCustomerID(userName)).ToList();
        }
    }

    public interface IShoppingCartDetailsService
    {
        int GetCount(string userName);
        List<ShoppingCartDetails> currentShoppingCart(string userName);
    }
}
