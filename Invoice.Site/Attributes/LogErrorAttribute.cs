using Invoice.Definitions;
using Ninject;
using Ninject.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http.Filters;
using System.Web.Mvc;

namespace Invoice.Site.Attributes
{
    public class LogErrorAttribute : HandleErrorAttribute
    {
        [Inject]
        public ILogger Logger { private get; set; }

        public override void OnException(ExceptionContext filterContext)
        {
            if (filterContext.ExceptionHandled || !filterContext.HttpContext.IsCustomErrorEnabled)
            {
                return;
            }

            if (IsAjax(filterContext))
            {
                filterContext.Result = new JsonResult()
                {
                    Data = new { success = false, errors = filterContext.Exception.Message },
                    JsonRequestBehavior = JsonRequestBehavior.AllowGet
                };

                filterContext.ExceptionHandled = true;
                filterContext.HttpContext.Response.Clear();
            }
            else
            {
                base.OnException(filterContext);
            }

            var currentController = (string)filterContext.RouteData.Values[Consts.MvcPattern.CONTROLLER];
            var currentActionName = (string)filterContext.RouteData.Values[Consts.MvcPattern.ACTION];

            log4net.ThreadContext.Properties[Consts.MvcPattern.CONTROLLER] = currentController;
            log4net.ThreadContext.Properties[Consts.MvcPattern.ACTION] = currentActionName;

            Logger.Error(filterContext.Exception.Message);
        }

        protected virtual bool IsAjax(ExceptionContext filterContext)
        {
            return filterContext.HttpContext.Request.Headers["X-Requested-With"] == "XMLHttpRequest";
        }

    }
}