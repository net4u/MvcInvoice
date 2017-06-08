using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Invoice.Site.Models.Company
{
    public class CompanySearchViewModel
    {
        public int PageSize { get; set; }

        public int PageIndex { get; set; }

        public string Name { get; set; }

        public string Symbol { get; set; }

        public bool InitSearch { get; set; }

        [DisplayName("Free Text")]
        [MaxLength(255, ErrorMessage = "Too much characters, max: 255")]
        public string FreeText { get; set; }

        [DisplayName("Sort By")]
        public int SelectedSortBy { get; set; }

        [DisplayName("Sort Order")]
        public int SelectedSortType { get; set; }

        public SelectList SortByList { get; set; }

        public SelectList SortTypeList { get; set; }
    }
}