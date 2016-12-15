using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Invoice.Definitions.Interfaces
{
    public interface ICurrencyData
    {
        string Name { get; set; }
        string Symbol { get; set; }
        Decimal Rate { get; set; }
    }
}
