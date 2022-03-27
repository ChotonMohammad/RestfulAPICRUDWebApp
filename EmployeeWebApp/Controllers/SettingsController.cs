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

    public class SettingsController : ApiController
    {

        /*****************************
        
           Dropdown Related Action Start

        *****************************/
        // GET: api/ShowMenuItemsForDropdown

        [HttpGet]
        [Route("api/ShowMenuItemsForDropdown")]
        public IHttpActionResult ShowMenuItemsForDropdown()
        {
            using (EmployeeDbContext dbContext = new EmployeeDbContext())
            {
                MenuItemsGateway menuItemsGateway = new MenuItemsGateway();
                var data = menuItemsGateway.GetAllMenuItem(dbContext);
                if (data == null)
                {
                    return Ok(new { error = true, message = "MenuItems data not found." });
                }
                return Ok(new { error = false, data });
            }
        }

        /*****************************
        
           Dropdown Related Action End

        *****************************/

        /*****************************
        
           Settings Related Action Start

        *****************************/


        // GET: api/GetSettings/10/1/1
        [Route("api/GetSettings/{limit}/{menuItemId}/{page}")]
        public IHttpActionResult GetSettings(int limit, int menuItemId, int page)
        {
            using (EmployeeDbContext dbContext = new EmployeeDbContext())
            {
                MenuSettingsGateway menuSettingsGateway = new MenuSettingsGateway();
                var data = menuSettingsGateway.GetAllMenuSettings(limit, menuItemId, page, dbContext);
                if (data == null)
                {
                    return Ok(new { error = true, message = "Settings data not found." });
                }
                return Ok(new { error = false, data });
            }
        }

        // GET: api/PostSetting

        [HttpPost]
        [Route("api/PostSetting")]
        public IHttpActionResult PostSetting(MenuSetting menuSetting)
        {
            if (!ModelState.IsValid)
            {
                return Ok(new { error = true, message = "Please give the information properly." });
            }

            using (EmployeeDbContext dbContext = new EmployeeDbContext())
            {
                MenuSettingsGateway menuSettingsGateway = new MenuSettingsGateway();
                int rowAffected = menuSettingsGateway.AddMenuSetting(menuSetting, dbContext);
                if (rowAffected > 0)
                {
                    return Ok(new { error = false, message = "Setting saved successfully." });
                }
                return Ok(new { error = true, message = "Error! Setting is not saved. Please try again later." });
            }
        }

        // GET: api/PutSetting

        [HttpPut]
        [Route("api/PutSetting")]

        public IHttpActionResult PutSetting(MenuSetting menuSetting)
        {
            if (menuSetting.MenuSettingId <= 0)
            {
                return Ok(new { error = true, message = "Error! Invalid request detected." });
            }
            if (!ModelState.IsValid)
            {
                return Ok(new { error = true, message = "Please give the information properly." });
            }

            using (EmployeeDbContext dbContext = new EmployeeDbContext())
            {
                MenuSettingsGateway menuSettingsGateway = new MenuSettingsGateway();

                int rowAffected = menuSettingsGateway.EditMenuSetting(menuSetting, dbContext);
                if (rowAffected > 0)
                {
                    return Ok(new { error = false, message = "Setting updated successfully." });
                }
                else if (rowAffected == 0)
                {
                    return Ok(new { error = true, message = "Setting is not updated. You did not change anything." });
                }
                else if (rowAffected == -3)
                {
                    return Ok(new { error = true, message = "Invalid request detected! Setting is not updated." });
                }
                return Ok(new { error = true, message = "Oops! Setting updation failed. Please try again later." });
            }

        }
    }
}
