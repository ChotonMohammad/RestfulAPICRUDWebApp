using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EmployeeWebApp.Controllers
{
    public class HomeController : Controller
    {
        [Route("")]
        public ActionResult SignIn()
        {
            return View();
        }
        public ActionResult Employees()
        {
            return View();
        }
        public ActionResult Settings()
        {
            return View();
        }
        public ActionResult TrashAndRecovery()
        {
            return View();
        }
    }
}
