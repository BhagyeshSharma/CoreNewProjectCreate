using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity.Modal
{
    public class tblDivision
    {
        [Key]
        public int DivisionId { get; set; }
        [Display(Name = "Division")]
        [StringLength(50)]
        [Column(TypeName = "nvarchar(50)")]
        public string ? DivisionName { get; set; }
    }
}
