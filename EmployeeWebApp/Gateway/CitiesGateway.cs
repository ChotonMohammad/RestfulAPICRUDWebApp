using EmployeeWebApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EmployeeWebApp.Gateway
{
    public class CitiesGateway
    {
        public List<City> GetAllCity(EmployeeDbContext dbContext)
        {
            try
            {
                var cities = dbContext.Cities.OrderBy(c => c.CityName)
                                .Where(c => c.IsDeleted == Utility.DeleteStatus.No)
                                .ToList();
                return cities;
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}