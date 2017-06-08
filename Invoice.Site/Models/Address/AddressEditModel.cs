using Invoice.Site.Models.Country;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Invoice.Site.Models.Address
{
    public class AddressEditModel
    {

        public int Id { get; set; }

        [DisplayName("City*")]
        [Required(ErrorMessage = "City is required")]
        [MaxLength(255, ErrorMessage = "Too much characters, max: 255")]
        public string City { get; set; }

        [DisplayName("Street*")]
        [Required(ErrorMessage = "Street is required")]
        [MaxLength(255, ErrorMessage = "Too much characters, max: 255")]
        public string Street { get; set; }

        [DisplayName("Building no.")]
        [MaxLength(255, ErrorMessage = "Too much characters, max: 255")]
        public string BuildingNumber { get; set; }

        [DisplayName("Home no.")]
        [MaxLength(255, ErrorMessage = "Too much characters, max: 255")]
        public string HomeNumber { get; set; }

        [DisplayName("Postal Code")]
        [MaxLength(255, ErrorMessage = "Too much characters, max: 255")]
        public string PostCode { get; set; }

        [DisplayName("Country*")]
        [Required(ErrorMessage = "Country is required")]
        public int SelectedCountryId { get; set; }

        public List<CountryViewModel> Countries { get; set; }
    }
}