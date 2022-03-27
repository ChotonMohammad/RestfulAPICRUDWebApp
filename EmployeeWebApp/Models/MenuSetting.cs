using EmployeeWebApp.Utility;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace EmployeeWebApp.Models
{
    public class MenuSetting
    {
        [Key]
        public int MenuSettingId { get; set; }
        [Index("UK_MenuSetting", IsUnique = true)]
        public int MenuItemId { get; set; }
        public ActiveStatus ViewStatus { get; set; } = ActiveStatus.Enabled;
        public ActiveStatus CreateStatus { get; set; } = ActiveStatus.Enabled;
        public ActiveStatus UpdateStatus { get; set; } = ActiveStatus.Enabled;
        public ActiveStatus DeleteStatus { get; set; } = ActiveStatus.Enabled;

        //nav property

        [ForeignKey("MenuItemId")]
        public MenuItem MenuItem { get; set; }
    }
}