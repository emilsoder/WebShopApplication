using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WebShopApp.Models;

namespace WebShopApp.Models
{
    public partial class Products
    {
        public Products()
        {
            OrderDetails = new HashSet<OrderDetails>();
            ShoppingCartDetails = new HashSet<ShoppingCartDetails>();
        }

        public int ProductID { get; set; }

        [Required(ErrorMessage = "Detta fält får inte vara tomt!")]
        [Display(Name = "Produktnamn")]
        public string ProductName { get; set; }

        [Required(ErrorMessage = "Detta fält får inte vara tomt!")]
        [Display(Name = "Beskrivning")]
        public string Description { get; set; }
        public string Ingredients { get; set; }

        [Required(ErrorMessage = "Detta fält får inte vara tomt!")]
        [Display(Name = "Pris/st")]
        [DataType(DataType.Currency)]
        public decimal UnitPrice { get; set; }
        public int? CategoryNumber { get; set; }

        public virtual ICollection<OrderDetails> OrderDetails { get; set; }
        public virtual ICollection<ShoppingCartDetails> ShoppingCartDetails { get; set; }
    }

   
}
