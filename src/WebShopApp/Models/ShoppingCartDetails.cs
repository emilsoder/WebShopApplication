using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebShopApp.Models
{
    public partial class ShoppingCartDetails
    {
        public int ShoppingCartID { get; set; }
        public int? ProductID { get; set; }
        public decimal? UnitPrice { get; set; }
        public int? Quantity { get; set; }
        public string ProductName { get; set; }
        public int ID { get; set; }

        public virtual Products Product { get; set; }
        public virtual ShoppingCarts ShoppingCart { get; set; }
    }
}
