using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity.Modal
{
    public class Tbl_Team
    {
        [Key]
        public int TeamId { get; set; }
        [Column(TypeName = "nvarchar(50)")]
        public string TeamName { get; set; }
        public int DepartmentId { get; set; }
        public List<Tbl_Employee> Employees { get; set; }
    }
}
