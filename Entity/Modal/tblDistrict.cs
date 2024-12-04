using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity.Modal
{
    public class tblDistrict
    {
        [Key]
        public int DistrictId { get; set; }
        [Display(Name ="District")]
        [StringLength(80)]
        [Column(TypeName ="nvarchar(80)")]
        public string DistrictName { get; set; } 
        public int DivisionId { get; set; }

        // Navigation property to access related Division
        [ForeignKey("DivisionId")] // Explicitly define the foreign key
        public tblDivision Division { get; set; }
    }
}
