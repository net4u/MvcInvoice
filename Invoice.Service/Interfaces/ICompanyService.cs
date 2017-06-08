using Invoice.Database;
using Invoice.Service.DataObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Invoice.Service.Interfaces
{
    public interface ICompanyService : IEntityService<Company, int>, ISearchService<Company, CompanySearch>
    {
        IEnumerable<Currency_SDIC> GetAllCurrencies();
        IEnumerable<Country_SDIC> GetAllCountries();
    }
}
