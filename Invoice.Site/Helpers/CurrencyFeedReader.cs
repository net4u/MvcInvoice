using Invoice.Definitions.Interfaces;
using Invoice.Site.Models.Currency;
using Ninject.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Helpers;

namespace Invoice.Site.Helpers
{
    public class CurrencyFeedReader : ICurrencyFeedReader
    {
        private ILogger _logger;

        public CurrencyFeedReader(ILogger logger)
        {
            _logger = logger;
        }

        public IEnumerable<ICurrencyData> GetFeeds(string address)
        {
            if (string.IsNullOrEmpty(address))
            {
                return null;
            }

            try
            {
                string response = string.Empty;

                using (WebClient webClient = new WebClient())
                {
                    webClient.Encoding = Encoding.UTF8;
                    webClient.Headers.Add("Accept: application/json");
                    response = webClient.DownloadString(address);
                }

                if (string.IsNullOrEmpty(response))
                {
                    return null;
                }

                dynamic json = Json.Decode(response);
                return ProcessResponse(json);
            }
            catch (Exception e)
            {
                _logger.Error(e.Message);
                return null;
            }
        }

        public async Task<IEnumerable<ICurrencyData>> Feeds(string address)
        {
            if (string.IsNullOrEmpty(address))
            {
                return null;
            }

            try
            {
                var response = await RequestData(address);
                if (string.IsNullOrEmpty(response))
                {
                    return null;
                }

                dynamic json = Json.Decode(response);
                return ProcessResponse(json);
            }
            catch (Exception e)
            {
                _logger.Error(e.Message);
                return null;
            }
        }

        protected IEnumerable<ICurrencyData> ProcessResponse(dynamic response)
        {
            List<ICurrencyData> result = new List<ICurrencyData>();
            int length = response[0].Rates.Length;
            for (int i = 0; i < length; i++)
            {
                result.Add(
                    new CurrencyFeedModel() { Name = response[0].Rates[i].Currency, Symbol = response[0].Rates[i].Code, Rate = response[0].Rates[i].Mid });
            }

            return result;
        }

        protected async Task<string> RequestData(string uri)
        {
            using (WebClient webClient = new WebClient())
            {
                webClient.Encoding = Encoding.UTF8;
                webClient.Headers.Add("Accept: application/json");
                return await webClient.DownloadStringTaskAsync(uri);
            }
        }
    }
}