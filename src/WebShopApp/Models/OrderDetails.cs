using WebShopApp.Models;

namespace WebShopApp.Models
{
    public partial class OrderDetails
    {
        public int OrderDetailsID { get; set; }
        public int? ProductID { get; set; }
        public int? Quantity { get; set; }
        public decimal? UnitPrice { get; set; }

        public virtual Orders OrderDetailsNavigation { get; set; }
        public virtual Products Product { get; set; }
    }
}