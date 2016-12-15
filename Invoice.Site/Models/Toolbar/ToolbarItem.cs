using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Invoice.Site.Models.Toolbar
{
    public class ToolbarItem
    {
        public string Name { get; set; }
        public string Controller { get; set; }
        public string Action { get; set; }
        public List<ToolbarItem> DropDownItems { get; set; }
    }
}