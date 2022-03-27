using EmployeeWebApp.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace EmployeeWebApp.Gateway
{
    public class EmployeesGateway
    {
        public dynamic GetAllEmployee(int limit, int departmentId, int cityId, int page,int deleteStatus, EmployeeDbContext dbContext)
        {
            try
            {
                var employees = dbContext.Employees.OrderByDescending(e => e.EmployeeId)
                                .Where(e => e.DepartmentId == departmentId || departmentId == -1)
                                .Where(e => e.CityId == cityId || cityId == -1)
                                .Where(e => e.IsDeleted==(Utility.DeleteStatus)deleteStatus)
                                .Include(e => e.Department)
                                .Include(e => e.City)
                                .Skip((page - 1) * limit)
                                .Take(limit)
                                .Select(em => new
                                {
                                    em.EmployeeId,
                                    em.EmployeeName,
                                    em.Gender,

                                    em.DepartmentId,
                                    em.Department.DepartmentName,

                                    em.CityId,
                                    em.City.CityName,
                                    em.IsDeleted
                                })
                                .ToList();
                return employees;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public dynamic GetAllEmployee(EmployeeDbContext dbContext)
        {
            try
            {
                var employees = dbContext.Employees.OrderByDescending(e => e.EmployeeId)
                                .Select(em => new
                                {
                                    em.EmployeeId,
                                    em.EmployeeName,
                                    em.Gender,

                                    em.DepartmentId,
                                    em.Department.DepartmentName,

                                    em.CityId,
                                    em.City.CityName,
                                    em.IsDeleted
                                })
                                .ToList();
                return employees;
            }
            catch (Exception)
            {
                return null;
            }
        }


        public int AddEmployee(Employee employee, EmployeeDbContext dbContext)
        {
            try
            {
                dbContext.Employees.Add(employee);
                return dbContext.SaveChanges();
            }
            catch (Exception)
            {
                return -2;
            }
        }

        public int EditEmployee(Employee employee, EmployeeDbContext dbContext)
        {

            try
            {
                var result = dbContext.Employees.SingleOrDefault(e => e.EmployeeId == employee.EmployeeId);
                if (result == null)
                {
                    return -3;
                }

                dbContext.Entry(result).CurrentValues.SetValues(employee);
                return dbContext.SaveChanges();
            }
            catch (Exception)
            {
                return -2;
            }
        }

        public int DeleteEmployee(int employeeId, EmployeeDbContext dbContext)
        {

            try
            {
                Employee employee = dbContext.Employees.SingleOrDefault(e => e.EmployeeId == employeeId);
                if (employee == null)
                {
                    return -3;
                }
                employee.IsDeleted = Utility.DeleteStatus.Yes;
                //dbContext.Employees.Remove(employee);
                return dbContext.SaveChanges();
            }
            catch (Exception)
            {
                return -2;
            }
        }

        public int RecoverEmployee(int employeeId, EmployeeDbContext dbContext)
        {

            try
            {
                Employee employee = dbContext.Employees.SingleOrDefault(e => e.EmployeeId == employeeId);
                if (employee == null)
                {
                    return -3;
                }
                employee.IsDeleted = Utility.DeleteStatus.No;
                //dbContext.Employees.Remove(employee);
                return dbContext.SaveChanges();
            }
            catch (Exception)
            {
                return -2;
            }
        }
    }
}