namespace EmployeeWebApp.Migrations
{
    using EmployeeWebApp.Models;
    using EmployeeWebApp.Utility;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<EmployeeWebApp.Models.EmployeeDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(EmployeeWebApp.Models.EmployeeDbContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method
            //  to avoid creating duplicate seed data.

            IList<Department> departments = new List<Department>();

            departments.Add(new Department() { DepartmentId = 1, DepartmentName = "Finance", IsDeleted = Utility.DeleteStatus.No });
            departments.Add(new Department() { DepartmentId = 2, DepartmentName = "Accounts", IsDeleted = Utility.DeleteStatus.No });
            departments.Add(new Department() { DepartmentId = 3, DepartmentName = "HR", IsDeleted = Utility.DeleteStatus.No });

            context.Departments.AddRange(departments);

            ///

            IList<City> cities = new List<City>();

            cities.Add(new City() { CityId = 1, CityName = "Dhaka", IsDeleted = Utility.DeleteStatus.No });
            cities.Add(new City() { CityId = 2, CityName = "Chittagong", IsDeleted = Utility.DeleteStatus.No });
            cities.Add(new City() { CityId = 3, CityName = "Comilla", IsDeleted = Utility.DeleteStatus.No });

            context.Cities.AddRange(cities);

            IList<MenuItem> menuItems = new List<MenuItem>();

            menuItems.Add(new MenuItem() { MenuItemId = 1, MenuItemName = "Employees", MenuItemShortName="employees" });

            context.MenuItems.AddRange(menuItems);
            
            IList<MenuSetting> menuSettings = new List<MenuSetting>();

            menuSettings.Add(new MenuSetting() {MenuSettingId=1, MenuItemId = 1, ViewStatus=ActiveStatus.Enabled ,CreateStatus=ActiveStatus.Enabled ,UpdateStatus=ActiveStatus.Enabled ,DeleteStatus=ActiveStatus.Enabled});

            context.MenuSettings.AddRange(menuSettings);

            base.Seed(context);
        }
    }
}
