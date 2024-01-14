using Microsoft.AspNetCore.Mvc;
//this is where the viewComp library comes from
using newAspProject.Models;

namespace newAspProject.Components.ViewComponents
{
    public class NavigationMenuComponent : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            var menuItems = new List<MenuItem>
            {
                new MenuItem{Controller = "Home", Action = "Index", Label = "Home"},
                new MenuItem{Controller = "Departments", Action = "Index", Label = "Departments"},
                new MenuItem{Controller = "Products", Action = "Index", Label = "Products"},
                new MenuItem{Controller = "Home", Action = "Privacy", Label = "Privacy"},
            };
            return View(menuItems);
        }
    }
}