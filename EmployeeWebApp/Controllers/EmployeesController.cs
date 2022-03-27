using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using EmployeeWebApp.Gateway;
using EmployeeWebApp.Models;

namespace EmployeeWebApp.Controllers
{
    [Authorize]
    public class EmployeesController : ApiController
    {

        /*****************************
        
           Dropdown Related Action Start

        *****************************/

        // GET: api/ShowDepartmentsForDropdown
        [HttpGet]
        [Route("api/ShowDepartmentsForDropdown")]
        public IHttpActionResult ShowDepartmentsForDropdown()
        {
            using (EmployeeDbContext dbContext = new EmployeeDbContext())
            {
                
                DepartmentsGateway departmentsGateway = new DepartmentsGateway();
                var data = departmentsGateway.GetAllDepartment(dbContext);
                if (data == null)
                {
                    return Ok(new { error = true, message = "Departments data not found." });
                }
                return Ok(new { error = false, data });
            }
        }

        // GET: api/ShowCitiesForDropdown

        [HttpGet]
        [Route("api/ShowCitiesForDropdown")]
        public IHttpActionResult ShowCitiesForDropdown()
        {
            using (EmployeeDbContext dbContext = new EmployeeDbContext())
            {
                CitiesGateway citiesGateway = new CitiesGateway();
                var data = citiesGateway.GetAllCity(dbContext);
                if (data == null)
                {
                    return Ok(new { error = true, message = "Cities data not found." });
                }
                return Ok(new { error = false, data });
            }
        }

        /*****************************
        
           Dropdown Related Action End

        *****************************/


        /*****************************
        
           Employee Related Action Start

        *****************************/

        // GET: api/Employees/10/1/1/1
        [Route("api/GetEmployees/{limit}/{departmentId}/{cityId}/{page}")]
        public IHttpActionResult GetEmployees(int limit, int departmentId, int cityId, int page)
        {
            using (EmployeeDbContext dbContext = new EmployeeDbContext())
            {
                if (!dbContext.MenuSettings.Where(x => x.ViewStatus == Utility.ActiveStatus.Enabled).Any())
                {
                    return Ok(new { error = true, message = "Setting disabled for view action." });
                }
                EmployeesGateway employeesGateway = new EmployeesGateway();
                int deleteStatus = 0;
                var data = employeesGateway.GetAllEmployee(limit,departmentId,cityId,page, deleteStatus, dbContext);
                if (data == null)
                {
                    return Ok(new { error = true, message = "Employees data not found." });
                }
                return Ok(new { error = false, data });
            }
        }

        // GET: api/PostEmployee

        [HttpPost]
        [Route("api/PostEmployee")]
        public IHttpActionResult PostEmployee(Employee employee)
        {
            if (!ModelState.IsValid)
            {
                return Ok(new { error = true, message = "Please give the information properly." });
            }

            using (EmployeeDbContext dbContext = new EmployeeDbContext())
            {
                if (!dbContext.MenuSettings.Where(x => x.CreateStatus == Utility.ActiveStatus.Enabled).Any())
                {
                    return Ok(new { error = true, message = "Setting disabled for create action." });
                }
                EmployeesGateway employeesGateway = new EmployeesGateway();

                int rowAffected = employeesGateway.AddEmployee(employee, dbContext);
                if (rowAffected>0)
                {
                    return Ok(new { error = false, message = "Employee saved successfully." });
                }
                return Ok(new { error = true, message = "Error! Employee is not saved. Please try again later." });
            }
        }

        // GET: api/PutEmployee

        [HttpPut]
        [Route("api/PutEmployee")]

        public IHttpActionResult PutEmployee(Employee employee)
        {
            if (employee.EmployeeId<=0)
            {
                return Ok(new { error = true, message = "Error! Invalid request detected." });
            }
            if (!ModelState.IsValid)
            {
                return Ok(new { error = true, message = "Please give the information properly." });
            }

            using (EmployeeDbContext dbContext = new EmployeeDbContext())
            {
                if (!dbContext.MenuSettings.Where(x => x.UpdateStatus == Utility.ActiveStatus.Enabled).Any())
                {
                    return Ok(new { error = true, message = "Setting disabled for update action." });
                }
                EmployeesGateway employeesGateway = new EmployeesGateway();

                int rowAffected = employeesGateway.EditEmployee(employee, dbContext);
                if (rowAffected > 0)
                {
                    return Ok(new { error = false, message = "Employee updated successfully." });
                }
                else if (rowAffected == 0)
                {
                    return Ok(new { error = true, message = "Employee is not updated. You did not change anything." });
                }
                else if (rowAffected == -3)
                {
                    return Ok(new { error = true, message = "Invalid request detected! Employee is not updated." });
                }
                return Ok(new { error = true, message = "Oops! Employee updation failed. Please try again later." });
            }

        }

        // GET: api/DeleteEmployee/1

        [HttpDelete]
        [Route("api/DeleteEmployee/{employeeId}")]
        public IHttpActionResult DeleteEmployee(int? employeeId)
        {
            if (employeeId == null || employeeId <= 0)
            {
                return Ok(new { error = true, message = "Error! Invalid request detected." });
            }
            using (EmployeeDbContext dbContext = new EmployeeDbContext())
            {
                if (!dbContext.MenuSettings.Where(x => x.DeleteStatus == Utility.ActiveStatus.Enabled).Any())
                {
                    return Ok(new { error = true, message = "Setting disabled for delete action." });
                }
                EmployeesGateway employeesGateway = new EmployeesGateway();
                int rowAffected = employeesGateway.DeleteEmployee(employeeId.Value, dbContext);
                if (rowAffected > 0)
                {
                    return Ok(new { error = false, message = "Employee deleted successfully." });
                }
                else if (rowAffected == -3)
                {
                    return Ok(new { error = true, message = "Oops! Employee deletion failed. Please try again later." });
                }
                return Ok(new { error = true, message = "Invalid request detected! Employee is not deleted." });
            }
        }


        /*******************************
         
          Employee Related Action End
         
         *****************************/

    }
}