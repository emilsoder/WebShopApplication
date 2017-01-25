using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebShopApp.Models;

namespace WebShopApp.Repositories
{
    public interface IProductRepository 
    {
        Task<IEnumerable<Products>> GetProducts();
        Task<Products> GetProductByID(int productID);
        void InsertProduct(Products product);
        void DeleteProduct(int productID);
        void UpdateProduct(Products product);
        Task<IEnumerable<Categories>> AllCategories();
    }
}
