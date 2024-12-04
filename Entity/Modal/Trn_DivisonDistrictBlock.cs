using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity.Modal
{
    public class Trn_DivisonDistrictBlock
    {
        [Key]
        public int ID { get; set; } 
        public int DivisionId { get; set; }
        public int DistrictId { get; set; }
        public int  BlockId { get; set; }
        [StringLength(100)]
        [Column(TypeName ="nvarchar(100)")]
        public string UserUploadfile { get; set; }  
    }
}
