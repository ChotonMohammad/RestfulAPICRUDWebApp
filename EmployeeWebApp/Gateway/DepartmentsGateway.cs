using EmployeeWebApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EmployeeWebApp.Gateway
{
    public class DepartmentsGateway
    {
        public List<Department> GetAllDepartment(EmployeeDbContext dbContext)
        {
            try
            {
                var departments = dbContext.Departments
                                .OrderBy(c => c.DepartmentName)
                                .Where(c => c.IsDeleted == Utility.DeleteStatus.No)
                                .ToList();
                return departments;
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}