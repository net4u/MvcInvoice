using Invoice.Site.Models.Address;
using Invoice.Site.Models.BankAccount;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Invoice.Site.Models.Company
{
    public class CompanyEditModel
    {
        public int Id { get; set; }
        public int BankAccountId { get; set; }
        public int AddressId { get; set; }

        [DisplayName("Name*")]
        [Required(ErrorMessage = "Name is required")]
        [MaxLength(255, ErrorMessage = "Too much characters, max: 255")]
        public string Name { get; set; }

        [DisplayName("Nip")]
        [MaxLength(15, ErrorMessage = "Too much characters, max: 15")]
        public string Nip { get; set; }

        [DisplayName("Regon")]
        [MaxLength(15, ErrorMessage = "Too much characters, max: 15")]
        public string Regon { get; set; }

        [DisplayName("Description")]
        [MaxLength(255, ErrorMessage = "Too much characters, max: 255")]
        public string Description { get; set; }

        [DisplayName("Symbol*")]
        [Required(ErrorMessage = "Symbol is required")]
        [MaxLength(10, ErrorMessage = "Too much characters, max: 10")]
        public string Symbol { get; set; }

        [DisplayName("E-mail")]
        [MaxLength(255, ErrorMessage = "Too much characters, max: 255")]
        public string Email { get; set; }

        public BankAccountEditModel BankAccount { get; set; }
        public AddressEditModel Address { get; set; }
        
    }
}