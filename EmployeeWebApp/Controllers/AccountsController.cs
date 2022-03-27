using EmployeeWebApp.Models;
using EmployeeWebApp.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace EmployeeWebApp.Controllers
{
    public class AccountsController : ApiController
    {
        // GET: login/admin/admin/admin

        [AllowAnonymous]
        [HttpPost]
        [Route("login/{userName}/{password}/{role}")]
        public IHttpActionResult Login(string userName, string password, string role)
        {
            if (userName == "admin" && password == "admin")
            {
                var token = JwtHelper.CreateJwtToken(userName, password,role);

                return Ok(new { success = true, jwt = token });
            }
            return Ok(new { success = false, msg = "Invalid credentials." });
        }
    }
}
