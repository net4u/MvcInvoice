using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Invoice.Definitions
{
    public static class Enums
    {
        public enum SortOrder
        {
            Ascending = 100,
            Descending = 200
        }

        public enum CompanySortBy
        {
            Name = 100,
            Symbol = 200,
            Nip = 300,
            Regon = 400
        }
    }
}
