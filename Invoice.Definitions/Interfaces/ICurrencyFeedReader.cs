using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Invoice.Definitions.Interfaces
{
    public interface ICurrencyFeedReader
    {
        IEnumerable<ICurrencyData> GetFeeds(string address);
        Task<IEnumerable<ICurrencyData>> Feeds(string address);
    }
}
