using EmployeeWebApp.Utility;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace EmployeeWebApp.Models
{
    public class Department
    {
        [Key]
        public int DepartmentId { get; set; }

        [Required]
        [Column(TypeName = "nvarchar")]
        [MaxLength(55)]
        [Index("UK_Department", IsUnique = true)]
        public string DepartmentName { get; set; }
        public DeleteStatus IsDeleted { get; set; } = DeleteStatus.No;


        // nav property

        public List<Employee> Employees { get; set; }
    }
}