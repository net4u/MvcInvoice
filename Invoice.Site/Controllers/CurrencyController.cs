using Invoice.Database;
using Invoice.Site.Helpers;
using Invoice.Definitions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Invoice.Database.Context;
using Invoice.Definitions.Interfaces;
using System.Threading.Tasks;
using Ninject.Extensions.Logging;
using Invoice.Site.Attributes;

namespace Invoice.Site.Controllers
{
    public partial class CurrencyController : Controller
    {
        private IUnitOfWork _unitOfWork;
        private ICurrencyFeedReader _reader;
        private ILogger _logger;

        public CurrencyController(IUnitOfWork unitOfWork, ICurrencyFeedReader reader, ILogger logger)
        {
            _unitOfWork = unitOfWork;
            _reader = reader;
            _logger = logger;
        }

        [AjaxOnly]
        public virtual async Task<ActionResult> Index()
        {
            try
            {
                ParameterGlobal parameter = _unitOfWork.ParameterGlobalRepository
                                                       .SingleOrDefault(e => e.KeyName == Consts.Param.BANK_CURRENCY_EXCHANGE_URL);
                if (parameter == null || string.IsNullOrEmpty(parameter.Value))
                {
                    return null;
                }

                var currencyList = _reader.Feeds(parameter.Value);
                if (currencyList == null)
                {
                    return null;
                }

                return PartialView(await currencyList);
            }
            catch (Exception e)
            {
                _logger.Error(e.Message);
                return null;
            }
        }
    }
}