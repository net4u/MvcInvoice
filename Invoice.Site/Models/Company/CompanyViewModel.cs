using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Invoice.Site.Models.Company
{
    public class CompanyViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Symbol { get; set; }
        public string Nip { get; set; }
    }
}