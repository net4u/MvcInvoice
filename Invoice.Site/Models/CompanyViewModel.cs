using Invoice.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Invoice.Site.Models
{
    public class CompanyViewModel
    {
        public CompanyModel Company { get; set; }
        public List<Currency_SDIC> Currencies;
        public List<Country_SDIC> Countries;
    }
}