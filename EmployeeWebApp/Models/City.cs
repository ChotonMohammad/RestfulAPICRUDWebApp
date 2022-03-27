using EmployeeWebApp.Utility;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace EmployeeWebApp.Models
{
    public class City
    {
        [Key]
        public int CityId { get; set; }

        [Required]
        [Column(TypeName = "nvarchar")]
        [MaxLength(50)]
        [Index("UK_City", IsUnique = true)]

        public string CityName { get; set; }
        public DeleteStatus IsDeleted { get; set; } = DeleteStatus.No;


        // nav property

        public List<Employee> Employees { get; set; }
    }
}