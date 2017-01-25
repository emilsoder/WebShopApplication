using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebShopApp.Models;

namespace WebShopApp.Repositories
{
    public class ShoppingCartDetailsRepository : IShoppingCartDetailsRepository
    {
        private ApplicationDbContext _context;
        public DbSet<ShoppingCartDetails> _shoppingCartDetails { get; set; }

        public ShoppingCartDetailsRepository(ApplicationDbContext context)
        {
            this._context = context;
            this._shoppingCartDetails = _context.Set<ShoppingCartDetails>();
        }

        public IEnumerable<ShoppingCartDetails> ShoppingCartDetailsByIDAsEnumerable(int shoppingCartID)
        {
            var shoppingCartDetails = _shoppingCartDetails.Where(x =>
              x.ShoppingCartID == shoppingCartID).AsEnumerable();
            return shoppingCartDetails;
        }

        public void RemoveItemFromCart(int id)
        {
            var itemToRemove = _shoppingCartDetails.SingleOrDefault(m => m.ID == id);
            _context.ShoppingCartDetails.Remove(itemToRemove);
            _context.SaveChanges();
        }

        public async void DeleteShoppingCartDetails(int shoppingCartID)
        {
            ShoppingCartDetails shoppingCartDetails = await _shoppingCartDetails.SingleOrDefaultAsync(x =>
                x.ShoppingCartID == shoppingCartID);
            _context.ShoppingCartDetails.Remove(shoppingCartDetails);
        }

        public int CountProductsInShoppingCart(int shoppingCartID)
        {
            return _shoppingCartDetails.Where(x => x.ShoppingCartID == shoppingCartID).Count();
        }
    }
}


