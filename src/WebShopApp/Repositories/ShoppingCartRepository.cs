using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebShopApp.Models;
using Microsoft.EntityFrameworkCore;

namespace WebShopApp.Repositories
{
    public class ShoppingCartRepository : IShoppingCartRepository
    {
        private ApplicationDbContext _context;
        public DbSet<ShoppingCarts> _shoppingCarts;
        public ShoppingCartRepository(ApplicationDbContext context)
        {
            this._context = context;
            this._shoppingCarts = _context.Set<ShoppingCarts>();
        }

        public async void InsertShoppingCart(int customerID)
        {
            _shoppingCarts.Add(new ShoppingCarts
            {
                CustomerID = customerID
            });
            await _context.SaveChangesAsync();
        }

        public async Task<bool> ShoppingCartExists(int customerID)
        {
            if (!await _shoppingCarts.AnyAsync(x => x.CustomerID == customerID))
            {
                return false;
            }
            return true;
        }

        public void Save()
        {
            _context.SaveChanges();
        }
    }
}
