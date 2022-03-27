using EmployeeWebApp.Gateway;
using EmployeeWebApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace EmployeeWebApp.Controllers
{
    [Authorize]

    public class TrashAndRecoveryController : ApiController
    {

        // GET: api/recovery/GetEmployees/10/1/2/1
        [Route("api/recovery/GetEmployees/{limit}/{departmentId}/{cityId}/{page}")]
        public IHttpActionResult GetEmployees(int limit, int departmentId, int cityId, int page)
        {
            using (EmployeeDbContext dbContext = new EmployeeDbContext())
            {
                EmployeesGateway employeesGateway = new EmployeesGateway();
                int deleteStatus = 1;
                var data = employeesGateway.GetAllEmployee(limit,departmentId,cityId,page,deleteStatus,dbContext);
                if (data == null)
                {
                    return Ok(new { error = true, message = "Employees data not found for recovery." });
                }
                return Ok(new { error = false, data });
            }
        }

        // GET: api/recovery/RecoverEmployee/1

        [HttpPost]
        [Route("api/recovery/RecoverEmployee/{employeeId}")]
        public IHttpActionResult RecoverEmployee(int? employeeId)
        {
            if (employeeId == null || employeeId <= 0)
            {
                return Ok(new { error = true, message = "Error! Invalid request detected." });
            }
            using (EmployeeDbContext dbContext = new EmployeeDbContext())
            {
                EmployeesGateway employeesGateway = new EmployeesGateway();
                int rowAffected = employeesGateway.RecoverEmployee(employeeId.Value, dbContext);
                if (rowAffected > 0)
                {
                    return Ok(new { error = false, message = "Employee recovered successfully." });
                }
                else if (rowAffected == -3)
                {
                    return Ok(new { error = true, message = "Oops! Employee recovered failed. Please try again later." });
                }
                return Ok(new { error = true, message = "Invalid request detected! Employee is not recovered." });
            }
        }


    }
}
