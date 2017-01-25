using System.Collections.Generic;
using System.Threading.Tasks;
using WebShopApp.Models;

namespace WebShopApp.Repositories
{
    public interface IShoppingCartRepository
    {
        void InsertShoppingCart(int customerID);
        Task<bool> ShoppingCartExists(int customerID);
        void Save();
    }
}
