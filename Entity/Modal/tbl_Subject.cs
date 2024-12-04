using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity.Modal
{
    public class tbl_Subject
    {
        [Key]
        public int SubjectId { get; set; }
        [StringLength(80)]
        [Column(TypeName = "nvarchar")]
        [Display(Name ="Subject Name")]
        public string SubjectName { get; set; } 
    }
}
