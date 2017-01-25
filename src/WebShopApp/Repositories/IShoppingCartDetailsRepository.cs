using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebShopApp.Models;

namespace WebShopApp.Repositories
{
    public interface IShoppingCartDetailsRepository
    {
        IEnumerable<ShoppingCartDetails> ShoppingCartDetailsByIDAsEnumerable(int shoppingCartID);
        void DeleteShoppingCartDetails(int shoppingCartID);
        int CountProductsInShoppingCart(int shoppingCartID);
        void RemoveItemFromCart(int id);
    }
}