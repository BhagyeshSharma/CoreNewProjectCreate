using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity.Modal
{
    public class Tbl_StudentVerification
    {
        [Key]
        public int StdVerifcnID { get; set; }
        [Display(Name = "Student Name")]
        [StringLength(80)]
        [Column(TypeName = "nvarchar(80)")]
        public string StdVerifcnName { get; set; }
        [Display (Name ="Rool No.")]
        [StringLength(15)]
        [Column(TypeName = "varchar(15)")]
        public string StdRollnumber { get; set; }   
        [Display(Name = "Email")]
        [StringLength(80)]
        [Column(TypeName = "nvarchar(80)")]
        public string StdVerifcnEmail { get; set; }
        [Display (Name = "Mobile No.")]
        [StringLength(15)]
        [Column(TypeName = "varchar(15)")]
        public string StdVerifcnMobileNo { get; set; }
        public bool IsVerified { get; set; }    
    }
}
