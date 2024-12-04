using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity.Modal
{
    public class Tbl_User
    {
        [Key]
        public int UserId { get; set; }
        [StringLength(80)]
        [Column(TypeName = "nvarchar(80)")]
        public string UserName { get; set; }
        [StringLength(80)]
        [Column(TypeName = "nvarchar(80)")]
        public string UserEmail { get; set; }
        [StringLength(50)]
        [Column(TypeName ="nvarchar(50)")]
        public string UserStreet { get; set; }
        [StringLength(50)]
        [Column(TypeName = "nvarchar(50)")]
        public string UserCity { get; set; }
        [StringLength(10)]
        [Column(TypeName = "nvarchar(10)")]
        public string ZipCode { get; set; }
        [StringLength(15)]
        [Column(TypeName = "varchar(15)")]
        public string Phone { get; set; }
        [StringLength(15)]
        [Column(TypeName = "varchar(15)")]
        public string EmergencyContact { get; set; }
    }
}
