using EmployeeWebApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;

namespace EmployeeWebApp.Gateway
{
    public class MenuItemsGateway
    {
        public List<EmployeeWebApp.Models.MenuItem> GetAllMenuItem(EmployeeDbContext dbContext)
        {
            try
            {
                var data = dbContext.MenuItems.OrderBy(c => c.MenuItemName)
                                //.Where(c => c.IsDeleted == Utility.DeleteStatus.No)
                                .ToList();
                return data;
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}