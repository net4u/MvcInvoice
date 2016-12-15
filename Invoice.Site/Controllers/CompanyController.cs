using Invoice.Database.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Invoice.Site.Controllers
{
    public partial class CompanyController : Controller
    {
        private IUnitOfWork _unitOfWork;

        public CompanyController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        // GET: Company
        public virtual ActionResult Index()
        {
            var companies = _unitOfWork.CompanyRepository.GetAll();

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
