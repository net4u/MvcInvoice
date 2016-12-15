using Invoice.Definitions.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Invoice.Site.Models.Currency
{
    public class CurrencyFeedModel : ICurrencyData
    {
        public string Name { get; set; }
        public string Symbol { get; set; }
        public Decimal Rate { get; set; }
    }
}