using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebShopApp.Models.ViewModels.ManageViewModels
{
    public class ManageViewModel
    {
        public Customers customer { get; set; }
        public ChangePasswordViewModel changePasswordViewModel { get; set; }
    }
}
