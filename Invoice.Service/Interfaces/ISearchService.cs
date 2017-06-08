using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Invoice.Service.Interfaces
{
    public interface ISearchService<T, U>
    {
        IEnumerable<T> Search(U searchCriteria);
    }
}
