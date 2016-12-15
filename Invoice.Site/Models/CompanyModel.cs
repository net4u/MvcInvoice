using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Invoice.Site.Models
{
    public class CompanyModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Nip { get; set; }
        public string Regon { get; set; }
        public string Description { get; set; }
        [Required(ErrorMessage = "Symbol is required")]
        public string Symbol { get; set; }
        public string Email { get; set; }
        public Nullable<int> BankAccountId { get; set; }
        public Nullable<int> AddressId { get; set; }

        #region BankAccount

        [DisplayName("Name")]
        public string BankName { get; set; }
        [DisplayName("Number")]
        public string BankNumber { get; set; }
        public Nullable<int> BankCurrencyId { get; set; }

        #endregion

        #region Address

        [DisplayName("City")]
        public string AddCity { get; set; }
        [DisplayName("Street")]
        public string AddStreet { get; set; }
        [DisplayName("Building number")]
        public string AddBuildingNumber { get; set; }
        [DisplayName("Home number")]
        public string AddHomeNumber { get; set; }
        [DisplayName("Post code")]
        public string AddPostCode { get; set; }
        public Nullable<int> AddCountryId { get; set; }

        #endregion
    }
}