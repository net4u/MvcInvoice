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

namespace Invoice.Site.Controllers
{
    public partial class CurrencyController : Controller
    {
        private IUnitOfWork _unitOfWork;
        private ICurrencyFeedReader _reader;

        public CurrencyController(IUnitOfWork unitOfWork, ICurrencyFeedReader reader)
        {
            _unitOfWork = unitOfWork;
            _reader = reader;
        }

        public virtual async Task<ActionResult> Index()
        {
            ParameterGlobal parameter = _unitOfWork.ParameterGlobalRepository
                                                   .SingleOrDefault(e => e.KeyName == Consts.Param.BANK_CURRENCY_EXCHANGE_URL);
            if (parameter == null || string.IsNullOrEmpty(parameter.Value))
            {
                return null;
            }
            var currencyList = _reader.Feeds(parameter.Value);
            return PartialView(await currencyList);
        }
    }
}