using EmployeeWebApp.Models;
using EmployeeWebApp.Utility;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace EmployeeWebApp.Gateway
{
    public class MenuSettingsGateway
    {
        public dynamic GetAllMenuSettings(int limit, int menuItemId, int page, EmployeeDbContext dbContext)
        {
            try
            {
                var data = dbContext.MenuSettings.OrderByDescending(e => e.MenuSettingId)
                                .Where(e => e.MenuItemId == menuItemId || menuItemId == -1)
                                //.Where(e => e.IsDeleted == Utility.DeleteStatus.No)
                                .Include(e => e.MenuItem)
                                .Skip((page - 1) * limit)
                                .Take(limit)
                                .Select(em => new
                                {
                                    em.MenuSettingId,
                                    em.MenuItemId,
                                    em.MenuItem.MenuItemName,

                                    em.ViewStatus,
                                    em.CreateStatus,
                                    em.UpdateStatus,
                                    em.DeleteStatus,
                                })
                                .ToList();
                return data;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public int AddMenuSetting(MenuSetting menuSetting, EmployeeDbContext dbContext)
        {
            try
            {
                dbContext.MenuSettings.Add(menuSetting);
                return dbContext.SaveChanges();
            }
            catch (Exception)
            {
                return -2;
            }
        }

        public int EditMenuSetting(MenuSetting menuSetting, EmployeeDbContext dbContext)
        {

            try
            {
                var result = dbContext.MenuSettings.SingleOrDefault(e => e.MenuSettingId == menuSetting.MenuSettingId);
                if (result == null)
                {
                    return -3;
                }

                dbContext.Entry(result).CurrentValues.SetValues(menuSetting);
                return dbContext.SaveChanges();
            }
            catch (Exception)
            {
                return -2;
            }
        }

        //public static bool IsActiveMenuSetting(string menuShortName,, EmployeeDbContext dbContext)
        //{

        //    try
        //    {
        //        var result = dbContext.MenuSettings.SingleOrDefault(e => e.MenuItem.MenuItemShortName == menuShortName);
        //        if (result == null)
        //        {
        //            return null;
        //        }
        //        return result;
        //    }
        //    catch (Exception)
        //    {
        //        return null;
        //    }
        //}

    }
}