using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Invoice.Database
{
    [MetadataType(typeof(BankAccountMetaData))]
    public partial class BankAccount
    {
        internal sealed class BankAccountMetaData
        {
            [ForeignKey("CurrencyId")]
            public Currency_SDIC Currency_SDIC
            {
                get;
                set;
            }

        }
    }
}
