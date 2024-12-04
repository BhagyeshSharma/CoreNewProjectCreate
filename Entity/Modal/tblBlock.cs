using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity.Modal
{
    public class tblBlock
    {
        [Key]
        public int BlockId { get; set; }
        [Display(Name = "Block")]
        [StringLength(100)]
        [Column(TypeName = "nvarchar(100)")]
        public string BlockName { get; set; }
        public int DistrictId { get; set; }
        [ForeignKey("DistrictId")] 
        public tblDistrict District { get; set; }
    }
}
