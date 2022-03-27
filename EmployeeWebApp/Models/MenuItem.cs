using EmployeeWebApp.Utility;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace EmployeeWebApp.Models
{
    public class MenuItem
    {
        [Key]
        public int MenuItemId { get; set; }

        [Required]
        [Column(TypeName = "nvarchar")]
        [MaxLength(50)]

        public string MenuItemName { get; set; }
        [Required]
        [Column(TypeName = "nvarchar")]
        [MaxLength(25)]
        [Index("UK_MenuItemShortName", IsUnique = true)]
        public string MenuItemShortName { get; set; }
        //public DeleteStatus IsDeleted { get; set; } = DeleteStatus.No;

        // nav property
        //public MenuSetting MenuSetting { get; set; }
    }
}