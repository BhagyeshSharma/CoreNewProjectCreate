using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity.Modal
{
    public class Tbl_PageElements
    {
        [Key]
        public int PageElmntsId { get; set; }   
        public int PageId { get; set; }
        [Column(TypeName ="nvarchar(150)")]
        public string ElementType { get; set; }
        [Column(TypeName = "nvarchar(150)")]
        public string Content { get; set; } 
    }
}
