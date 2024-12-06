using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity.Modal
{
    public class Tbl_Employee
    {
        [Key]
        public int EmployeeID { get; set; } 
        [Column(TypeName ="nvarchar(80)")]
        public string EmployeeName { get; set; } 
        public int TeamId { get; set; }
    }
}
