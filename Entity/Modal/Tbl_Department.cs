using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity.Modal
{
    public class Tbl_Department
    {
        [Key]
        public int DepartmentID { get; set; }
        [Column(TypeName = "nvarchar(50)")]
        public string DepartmentName { get; set; }
        public List<Tbl_Team> Teams { get; set; }
    }
}
