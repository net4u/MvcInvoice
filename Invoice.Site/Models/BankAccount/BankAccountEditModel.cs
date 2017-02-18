using Invoice.Site.Models.Currency;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Invoice.Site.Models.BankAccount
{
    public class BankAccountEditModel
    {
        public int Id { get; set; }

        [DisplayName("Name")]
        [Required(ErrorMessage = "Header is required")]
        [MaxLength(255, ErrorMessage = "Too much characters, max: 255")]
        public string BankName { get; set; }

        [DisplayName("Number*")]
        [Required(ErrorMessage = "Number is required")]
        [MaxLength(26, ErrorMessage = "Too much characters, max: 26")]
        public string Number { get; set; }

        [DisplayName("Currency*")]
        [Required(ErrorMessage = "Currency is required")]
        public int SelectedCurrencyId { get; set; }

        public List<CurrencyViewModel> Currencies { get; set; }
    }
}