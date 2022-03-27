using EmployeeWebApp.Utility;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace EmployeeWebApp.Models
{
    public class Employee
    {
        [Key]
        public int EmployeeId { get; set; }

        [Required]
        [Column(TypeName = "nvarchar")]
        [MaxLength(55)]
        public string EmployeeName { get; set; }


        [Required]
        public Gender Gender { get; set; } = Gender.NotKnown;

        [Required]
        public int DepartmentId { get; set; }

        [Required]
        public int CityId { get; set; }

        public DeleteStatus IsDeleted { get; set; } = DeleteStatus.No;


        //nav property

        [ForeignKey("DepartmentId")]
        public Department Department { get; set; }

        [ForeignKey("CityId")]
        public City City { get; set; }
    }
}