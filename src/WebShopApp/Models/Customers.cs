using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WebShopApp.Models;

namespace WebShopApp.Models
{
    public partial class Customers
    {
        public Customers()
        {
            OrdersNavigation = new HashSet<Orders>();
            ShoppingCarts = new HashSet<ShoppingCarts>();
        }

        public int CustomerID { get; set; }

        [Required(ErrorMessage = "Detta fält får inte vara tomt!")]
        [Display(Name = "Förnamn")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Detta fält får inte vara tomt!")]
        [Display(Name = "Efternamn")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Detta fält får inte vara tomt!")]
        [Display(Name = "Telefonnummer")]
        public string Phone { get; set; }

        [Required(ErrorMessage = "Detta fält får inte vara tomt!")]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Detta fält får inte vara tomt!")]
        [Display(Name = "Leveransaddress")]
        public string Address { get; set; }

        public int? Orders { get; set; }

        [Required(ErrorMessage = "Detta fält får inte vara tomt!")]
        [Display(Name = "Användartyp")]
        public string Role { get; set; }

        public virtual ICollection<Orders> OrdersNavigation { get; set; }
        public virtual ICollection<ShoppingCarts> ShoppingCarts { get; set; }
    }
}
