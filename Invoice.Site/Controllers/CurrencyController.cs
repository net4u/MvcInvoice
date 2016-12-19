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
using System.IO;

namespace Invoice.Site.Controllers
{
    public partial class CurrencyController : BaseController
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
            string errorMsg = string.Empty;

            try
            {
                ParameterGlobal parameter = _unitOfWork.ParameterGlobalRepository
                                                       .SingleOrDefault(e => e.KeyName == Consts.Param.BANK_CURRENCY_EXCHANGE_URL);
                if (parameter == null || string.IsNullOrEmpty(parameter.Value))
                {
                   errorMsg = "No parameter for currency list defined";
                   _logger.Error(errorMsg);
                   Json(new { success = false, errors = errorMsg });
                }

                var currencyList = _reader.Feeds(parameter.Value);
                if (currencyList == null)
                {
                    errorMsg = "No currencies found";
                    _logger.Error(errorMsg);
                    Json(new { success = false, errors = errorMsg });
                }

                //return PartialView(await currencyList);
                return Json(new {success = true, data = RenderPartialViewToString(T4MVC_CurrencyController.s_views.ViewNames.Index, await currencyList)}, 
                    JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                _logger.Error(e.Message);
                return Json(new { success = false, errors = e.Message });
            }
        }

    }
}