using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebShopApp.Models.ViewModels.AccountViewModels
{
    public class ResetPasswordViewModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [Display(Name = "Nytt lösenord")]
        [StringLength(100, ErrorMessage = "Lösenordet måste vara minst fyra tecken långt", MinimumLength = 4)]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Bekräfta nytt lösenord")]
        [Compare("Password", ErrorMessage = "Det nya lösenordet och bekräftelselösenordet matchar inte.")]
        public string ConfirmPassword { get; set; }

        public string Code { get; set; }
    }
}
