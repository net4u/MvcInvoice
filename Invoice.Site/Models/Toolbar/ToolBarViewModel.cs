using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Invoice.Site.Models.Toolbar
{
    public class ToolBarViewModel
    {
        public string Name { get; set; }
        public List<ToolbarItem> Items { get; set; }
    }
}