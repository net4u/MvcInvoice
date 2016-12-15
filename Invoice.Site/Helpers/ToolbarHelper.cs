using Invoice.Site.Models.Toolbar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Invoice.Site.Helpers
{
    public static class ToolbarHelper
    {
        public static ToolBarViewModel GetToolbarMenuModel()
        {
            var toolbarModel = new ToolBarViewModel() { Name = "Home"};
            var toolbarItems = new List<ToolbarItem>();
            toolbarItems.Add(new ToolbarItem() { Name = "Home", Action = MVC.Home.ActionNames.Index, Controller = MVC.Home.Name });
            toolbarItems.Add(new ToolbarItem() { Name = "About", Action = MVC.Home.ActionNames.About, Controller = MVC.Home.Name });
            toolbarItems.Add(new ToolbarItem() { Name = "Contact", Action = MVC.Home.ActionNames.Contact, Controller = MVC.Home.Name });
            toolbarItems.Add(new ToolbarItem() { Name = "Currency", Action = MVC.Currency.ActionNames.Index, Controller = MVC.Currency.Name });

            var listedActions = new ToolbarItem() { Name = "List", DropDownItems = new List<ToolbarItem>() };
            listedActions.DropDownItems.Add(new ToolbarItem() 
                { Name = "HomeList", Action = MVC.Home.ActionNames.Index, Controller = MVC.Home.Name });
            listedActions.DropDownItems.Add(new ToolbarItem() 
                { Name = "HomeList2", Action = MVC.Home.ActionNames.Index, Controller = MVC.Home.Name });
            toolbarItems.Add(listedActions);

            toolbarModel.Items = toolbarItems;

            return toolbarModel;
        }
    }
}