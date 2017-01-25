using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WebShopApp.Models.AdministratorViewModels
{
    public class AdministratorViewModel
    {
        public Products OneProduct { get; set; }
        public IEnumerable<Products> AllProducts { get; set; }

        public Customers OneCustomer { get; set; }
        public IEnumerable<Customers> AllCustomers { get; set; }

        public ProductsViewModel OneProductVM { get; set; }

        public IEnumerable<Orders> orders { get; set; }

        public ProductViewModel CreateProduct { get; set; }
    }

    public class ProductViewModel
    {
        [Required(AllowEmptyStrings = false, ErrorMessage = "Detta fält får inte vara tomt!")]
        [Display(Name = "Produktnamn")]
        public string ProductName { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Detta fält får inte vara tomt!")]
        [Display(Name = "Beskrivning")]
        public string Description { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Detta fält får inte vara tomt!")]
        [Display(Name = "Pris/st")]
        [DataType(DataType.Currency)]

        public decimal UnitPrice { get; set; }
        public int? CategoryNumber { get; set; }
    }

    public class ProductsViewModel
    {
        [Required(AllowEmptyStrings = false, ErrorMessage = "Detta fält får inte lämnas tomt")]
        public string CategoryName { get; set; }
    }
}
