using AutoMapper;
using Invoice.Database.Context;
using Invoice.Site.Models.Company;
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

namespace Invoice.Site.Controllers
{
    [LogError(View = Consts.ErrorViewName.Error)]
    public partial class CompanyController : Controller
    {
        private IUnitOfWork _unitOfWork;
        private IMapper _mapper;
        private ILogger _logger;

        public CompanyController(IUnitOfWork unitOfWork, IMapper mapper, ILogger logger)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = logger;
        }

        // GET: Company
        public virtual ActionResult Index()
        {
            var criteria = new CompanySearchViewModel() { PageIndex = 1, PageSize = 10, 
                SortByList = Enums.CompanySortBy.Name.ToSelectList(), SortTypeList = Enums.SortOrder.Ascending.ToSelectList()};
            return View(criteria);
        }

        public virtual ActionResult Search(CompanySearchViewModel searchCriteria)
        {
            Session[Consts.SessionDataType.SEARCH_CRITERIA] = searchCriteria;

            var dbCompanies = _unitOfWork
               .CompanyRepository
               .AsQueryable()
               .Include(e => e.Address)
               .Include(e => e.BankAccount)
               .Include(e => e.BankAccount.Currency_SDIC)
               .Where(e => (string.IsNullOrEmpty(searchCriteria.Name) || e.Name.Contains(searchCriteria.Name)) && 
               (string.IsNullOrEmpty(searchCriteria.Symbol) || e.Symbol.Contains(searchCriteria.Symbol)) &&
               (string.IsNullOrEmpty(searchCriteria.FreeText) || true));

            var count = dbCompanies.Count();

            bool isAsc = searchCriteria.SelectedSortType == (int)Enums.SortOrder.Ascending ? true : false;

            switch (searchCriteria.SelectedSortBy)
            {
                case (int)Enums.CompanySortBy.Name:
                    dbCompanies = isAsc ? dbCompanies.OrderBy(e => e.Name) : dbCompanies.OrderByDescending(e => e.Name);
                    break;
                case (int)Enums.CompanySortBy.Nip:
                    dbCompanies = isAsc ? dbCompanies.OrderBy(e => e.Nip) : dbCompanies.OrderByDescending(e => e.Nip);
                    break;
                case (int)Enums.CompanySortBy.Regon:
                    dbCompanies = isAsc ? dbCompanies.OrderBy(e => e.Regon) : dbCompanies.OrderByDescending(e => e.Regon);
                    break;
                case (int)Enums.CompanySortBy.Symbol:
                    dbCompanies = isAsc ? dbCompanies.OrderBy(e => e.Symbol) : dbCompanies.OrderByDescending(e => e.Symbol);
                    break;
                default:
                    break;
            }

            var companies = dbCompanies
                .Skip(searchCriteria.PageIndex * searchCriteria.PageSize)
                .Take(searchCriteria.PageSize)
                .ToList<Company>();

            return View(companies);
        }

        // GET: Company/Details/5
        public virtual ActionResult Details(int id)
        {
            return View();
        }

        // GET: Company/Create
        public virtual ActionResult Create()
        {
            return View();
        }

        // POST: Company/Create
        [HttpPost]
        public virtual ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction(CompanyController.ActionNameConstants.Index);
            }
            catch
            {
                return View();
            }
        }

        // GET: Company/Edit/5
        public virtual ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Company/Edit/5
        [HttpPost]
        public virtual ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Company/Delete/5
        public virtual ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Company/Delete/5
        [HttpPost]
        public virtual ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
