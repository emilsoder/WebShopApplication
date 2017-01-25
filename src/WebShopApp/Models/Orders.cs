using System;
using WebShopApp.Models;

namespace WebShopApp.Models
{
    public partial class Orders
    {
        public int OrderID { get; set; }
        public int? CustomerID { get; set; }
        public DateTime? OrderDate { get; set; }
        public string Delivery { get; set; }
        public int? ProductsQty { get; set; }
        public decimal? NetPrice { get; set; }
        public decimal? Discount { get; set; }
        public decimal? TotalPrice { get; set; }

        public virtual OrderDetails OrderDetails { get; set; }
        public virtual Customers Customer { get; set; }
    }
}