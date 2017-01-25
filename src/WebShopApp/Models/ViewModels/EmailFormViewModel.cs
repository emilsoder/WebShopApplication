using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebShopApp.Models.ViewModels
{
    public class EmailFormViewModel
    {

        [Display(Name = "Ditt namn")]
        [Required(ErrorMessage = "Du måste ange ditt namn eller företagsnamn")]
        public string FromName { get; set; }

        [Display(Name = "Din epostaddress")]
        [Required(ErrorMessage = "Du måste ange en giltig epostaddress")]
        [EmailAddress(ErrorMessage = "Du måste ange en giltig epostaddress")]
        public string FromEmail { get; set; }

        [Display(Name = "Meddelande")]
        [MinLength(5), MaxLength(1000)]
        [Required(ErrorMessage = "Du måste skriva ett meddelande. Minst 10 och max 1000 tecken långt")]
        public string Message { get; set; }

        [Display(Name = "Ämne")]
        [Required(ErrorMessage = "Du måste ange ett ämne")]
        public string Subject { get; set; }

        [Display(Name = "Kategori")]
        [Required(ErrorMessage = "Du måste välja en kategori")]
        public string Category { get; set; }

    }
}
