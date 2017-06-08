using AutoMapper;
using Invoice.Database;
using Invoice.Service.DataObjects;
using Invoice.Site.Models.Address;
using Invoice.Site.Models.BankAccount;
using Invoice.Site.Models.Company;
using Invoice.Site.Models.Country;
using Invoice.Site.Models.Currency;
using Invoice.Site.Models.Post;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace Invoice.Site.Helpers
{
    public class AutoMapperProfile : Profile
    {
        protected override void Configure()
        {
            #region Post

            CreateMap<Post, PostViewModel>()
                .ForMember(a => a.ImageBaseStr, o => o.ResolveUsing(a =>
                    {
                        return System.Convert.ToBase64String(a.Image, 0, a.Image.Length);
                    }))
                .ForMember(a => a.Categories, o => o.MapFrom(a => a.PostCategory_SDIC));
            CreateMap<PostEditModel, Post>()
                .ForMember(a => a.Image, o => o.MapFrom(a => a.File))
                .ForMember(a => a.PostCategory_SDIC, o => o.Ignore());
            CreateMap<HttpPostedFileBase, byte[]>().ConstructUsing(a =>
                {
                    MemoryStream target = new MemoryStream();
                    a.InputStream.CopyTo(target);
                    return target.ToArray();
                });

            #endregion

            #region BankAccount

            CreateMap<BankAccountEditModel, BankAccount>()
                .ForMember(a => a.CurrencyId, o => o.MapFrom(a => a.SelectedCurrencyId))
                .ForMember(a => a.Customer, o => o.Ignore())
                .ForMember(a => a.DataSearch, o => o.Ignore())
                .ForMember(a => a.Company, o => o.Ignore())
                .ForMember(a => a.Currency_SDIC, o => o.Ignore())
                .ForMember(a => a.Id, o => o.Ignore());
            CreateMap<BankAccount, BankAccountEditModel>()
                .ForMember(a => a.SelectedCurrencyId, o => o.MapFrom(a => a.CurrencyId))
                .ForMember(a => a.Currencies, o => o.Ignore())
;
            #endregion

            #region Address

            CreateMap<AddressEditModel, Address>()
                .ForMember(a => a.CountryId, o => o.MapFrom(a => a.SelectedCountryId))
                .ForMember(a => a.Customer, o => o.Ignore())
                .ForMember(a => a.DataSearch, o => o.Ignore())
                .ForMember(a => a.Company, o => o.Ignore())
                .ForMember(a => a.Id, o => o.Ignore());
            CreateMap<Address, AddressEditModel>()
                .ForMember(a => a.SelectedCountryId, o => o.MapFrom(a => a.CountryId))
                .ForMember(a => a.Countries, o => o.Ignore());

            #endregion

            #region Company

            CreateMap<CompanySearchViewModel, CompanySearch>()
                .ForMember(a => a.SortBy, o => o.MapFrom(a => a.SelectedSortBy))
                .ForMember(a => a.SortType, o => o.MapFrom(a => a.SelectedSortType));
            CreateMap<CompanyEditModel, Company>()
                .ForMember(a => a.DataSearch, o => o.Ignore())
                .ForMember(a => a.Customer, o => o.Ignore())
                .ForMember(a => a.Invoice, o => o.Ignore())
                .ForMember(a => a.Id, o => o.Ignore())
                .ForMember(a => a.BankAccountId, o => o.Ignore())
                .ForMember(a => a.AddressId, o => o.Ignore());
            CreateMap<Company, CompanyEditModel>();
            CreateMap<Company, CompanyViewModel>();

            #endregion

            #region system dictionaries

            CreateMap<PostCategory_SDIC, PostCategoryViewModel>();
            CreateMap<Currency_SDIC, CurrencyViewModel>();
            CreateMap<Country_SDIC, CountryViewModel>();

            #endregion
        }

    }
}