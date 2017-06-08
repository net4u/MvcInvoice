using Invoice.Database;
using Invoice.Database.Interfaces;
using Invoice.Service.Base;
using Invoice.Service.DataObjects;
using Invoice.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using Invoice.Definitions;

namespace Invoice.Service.Services
{
    public class CompanyService : EntityService<Company, int>, ICompanyService
    {
        public IEnumerable<Company> Search(CompanySearch searchCriteria)
        {
            var dbCompanies = 
                _dbSet
                .AsQueryable()
                .Include(e => e.Address)
                .Include(e => e.BankAccount)
                .Include(e => e.BankAccount.Currency_SDIC)
                .Where(e => (string.IsNullOrEmpty(searchCriteria.Name) || e.Name.Contains(searchCriteria.Name)) &&
                (string.IsNullOrEmpty(searchCriteria.Symbol) || e.Symbol.Contains(searchCriteria.Symbol)) &&
                (string.IsNullOrEmpty(searchCriteria.FreeText) || true));

            bool isAsc = searchCriteria.SortType == (int)Enums.SortOrder.Ascending ? true : false;

            switch (searchCriteria.SortBy)
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

            return dbCompanies
                .Skip(searchCriteria.PageIndex * searchCriteria.PageSize)
                .Take(searchCriteria.PageSize);
        }

        public CompanyService(IContext dbContext)
            : base(dbContext)
        {
            _dbContext = dbContext;
            _dbSet = _dbContext.Set<Company>();
        }

        public override Company GetById(int id)
        {
            return _dbSet
                    .Include(e => e.Address)
                    .Include(e => e.BankAccount)
                    .Include(e => e.BankAccount.Currency_SDIC)
                    .SingleOrDefault(e => e.Id == id);
        }

        public override IEnumerable<Company> GetAll()
        {
     	    return _dbSet
                    .Include(e => e.Address)
                    .Include(e => e.BankAccount)
                    .Include(e => e.BankAccount.Currency_SDIC);
        }

        public IEnumerable<Currency_SDIC> GetAllCurrencies()
        {
            return _dbContext.CurrencySdicRepository.OrderBy(e => e.OrderNumber);
        }

        public IEnumerable<Country_SDIC> GetAllCountries()
        {
            return _dbContext.CountrySdicRepository.OrderBy(e => e.OrderNumber);
        }
    }
}
