using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebShopApp.Models
{
    public partial class ShoppingCarts
    {
        public ShoppingCarts()
        {
            ShoppingCartDetails = new HashSet<ShoppingCartDetails>();
        }

        public int ShoppingCartID { get; set; }
        public int? CustomerID { get; set; }

        public virtual ICollection<ShoppingCartDetails> ShoppingCartDetails { get; set; }
        public virtual Customers Customer { get; set; }
    }
}
