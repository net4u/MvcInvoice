using AutoMapper;
using Invoice.Database.Context;
using Invoice.Site.Models.Company;
using Invoice.Site.Models.BankAccount;
using Invoice.Site.Models.Address;
using Ninject.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using Invoice.Definitions;
using Invoice.Database;
using Invoice.Site.Extensions;
using Invoice.Site.Attributes;
using Invoice.Service.Interfaces;
using Invoice.Service.DataObjects;
using Invoice.Site.Models.Country;
using Invoice.Site.Models.Currency;

namespace Invoice.Site.Controllers
{
    [LogError(View = Consts.ErrorViewName.Error)]
    public partial class CompanyController : BaseController
    {
        private ICompanyService _companies;
        private IMapper _mapper;
        private ILogger _logger;

        public CompanyController(ICompanyService companies, IMapper mapper, ILogger logger)
        {
            _companies = companies;
            _mapper = mapper;
            _logger = logger;
        }

        public virtual ActionResult Index()
        {
            var criteria = new CompanySearchViewModel()
            {
                PageIndex = 0,
                PageSize = 10,
                SortByList = Enums.CompanySortBy.Name.ToSelectList(),
                SortTypeList = Enums.SortOrder.Ascending.ToSelectList()
            };
            return View(criteria);
        }

        public virtual ActionResult Search(CompanySearchViewModel searchCriteria)
        {
            Session[Consts.SessionDataType.SEARCH_CRITERIA] = searchCriteria;

            var dbCompanies = _companies.Search(_mapper.Map<CompanySearch>(searchCriteria));
            var companies = _mapper.Map<List<CompanyViewModel>>(dbCompanies);
            var model = new CompanySearchResultViewModel() { SearchResultList = companies };

            if (ControllerContext.HttpContext.Request.IsAjaxRequest())
            {
                return Json(new { success = true, data = RenderPartialViewToString(MVC.Company.Views.Search, model) },
                    JsonRequestBehavior.AllowGet);
            }
            else
            {
                return View(model);
            }
        }

        public virtual ActionResult Create()
        {
            var model = new CompanyEditModel() { BankAccount = new BankAccountEditModel(), Address = new AddressEditModel() };
            var currencies = _companies.GetAllCurrencies();
            model.BankAccount.Currencies = _mapper.Map<List<CurrencyViewModel>>(currencies);
            var countries = _companies.GetAllCountries();
            model.Address.Countries = _mapper.Map<List<CountryViewModel>>(countries);
            return View(model);
        }

        [HttpPost]
        public virtual ActionResult Create(CompanyEditModel model)
        {
            var company = _mapper.Map<Company>(model);
            _companies.Add(company);
            _companies.Commit();
            CompanySearchViewModel searchCriteria = (CompanySearchViewModel)Session[Consts.SessionDataType.SEARCH_CRITERIA];
            if (searchCriteria == null)
            {
                searchCriteria = new CompanySearchViewModel()
                {
                    PageIndex = 1,
                    PageSize = 10,
                    SortByList = Enums.CompanySortBy.Name.ToSelectList(),
                    SortTypeList = Enums.SortOrder.Ascending.ToSelectList()
                };
            }
            searchCriteria.InitSearch = true;

            return View(MVC.Company.Views.Index, searchCriteria);
        }

        public virtual ActionResult Edit(int id)
        {
            var dbCompany = _companies.GetById(id);
            var model = _mapper.Map<CompanyEditModel>(dbCompany);
            var currencies = _companies.GetAllCurrencies();
            model.BankAccount.Currencies = _mapper.Map<List<CurrencyViewModel>>(currencies);
            var countries = _companies.GetAllCountries();
            model.Address.Countries = _mapper.Map<List<CountryViewModel>>(countries);
            return View(model);
        }

        [HttpPost]
        public virtual ActionResult Edit(CompanyEditModel model)
        {
            var company = _companies.GetById(model.Id);
           _mapper.Map(model, company);
            _companies.Commit();
            CompanySearchViewModel searchCriteria = (CompanySearchViewModel)Session[Consts.SessionDataType.SEARCH_CRITERIA];
            if (searchCriteria == null)
            {
                searchCriteria = new CompanySearchViewModel()
                {
                    PageIndex = 1,
                    PageSize = 10,
                    SortByList = Enums.CompanySortBy.Name.ToSelectList(),
                    SortTypeList = Enums.SortOrder.Ascending.ToSelectList()
                };
            }
            searchCriteria.SortByList = Enums.CompanySortBy.Name.ToSelectList();
            searchCriteria.SortTypeList = Enums.SortOrder.Ascending.ToSelectList();
            searchCriteria.InitSearch = true;

            return View(MVC.Company.Views.Index, searchCriteria);
        }

        [HttpPost]
        [AjaxOnly]
        public virtual JsonResult Delete(int id)
        {
            var company = _companies.GetById(id);
            _companies.Delete(company);
            _companies.Commit();
            return Json(new { success = true });
        }
    }
}
