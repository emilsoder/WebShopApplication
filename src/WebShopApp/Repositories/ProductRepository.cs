using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebShopApp.Models;

namespace WebShopApp.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private ApplicationDbContext _context;
        public DbSet<Products> products { get; set; }

        public ProductRepository(ApplicationDbContext context)
        {
            this._context = context;
            this.products = _context.Set<Products>();
        }

        public async Task<IEnumerable<Products>> GetProducts()
        {
            return await products.ToListAsync();
        }

        public async Task<Products> GetProductByID(int productID)
        {
            return await products.SingleOrDefaultAsync(x =>
                x.ProductID == productID);
        }

        public void InsertProduct(Products product)
        {
            products.Add(product);
            _context.SaveChanges();
        }

        public async Task<IEnumerable<Categories>> AllCategories()
        {
            return await _context.Categories.ToListAsync();
        }

        public void DeleteProduct(int productID)
        {
            Products product = products.SingleOrDefault(x => x.ProductID == productID);
            products.Remove(product);
            _context.SaveChanges();
        }

        public void UpdateProduct(Products product)
        {
            try
            {
                Products productToEdit = products.Find(product.ProductID);
                productToEdit.ProductName = product.ProductName;
                productToEdit.Description = product.Description;
                productToEdit.UnitPrice = product.UnitPrice;
                productToEdit.CategoryNumber = product.CategoryNumber;
                _context.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                return;
            }
        }
    }
}