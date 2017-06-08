using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Invoice.Service.DataObjects
{
    public class CompanySearch
    {
        public int PageSize { get; set; }

        public int PageIndex { get; set; }

        public string Name { get; set; }

        public string Symbol { get; set; }

        public string FreeText { get; set; }

        public int SortBy { get; set; }

        public int SortType { get; set; }
    }
}
