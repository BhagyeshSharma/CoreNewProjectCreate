using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entity.Modal
{
    public class StudentReg
    {
        [Key]
        public int StudentId { get; set; }

        [Display(Name = "Student Roll no")]
        [StringLength(15)]
        [Column(TypeName = "varchar(15)")]
        public string StudentRollNo { get; set; }

        [Display(Name = "Student Name")]
        [StringLength(100)]
        [Column(TypeName = "nvarchar(100)")] // Specifying length for nvarchar
        public string StudentName { get; set; }

        [Display(Name = "Student Email")]
        [StringLength(254)] // Increased length for emails
        [Column(TypeName = "varchar(254)")]
        public string StudentEmail { get; set; }

        [Display(Name = "Student Mobile no")]
        [StringLength(15)]
        [Column(TypeName = "varchar(15)")]
        public string StudentMobNo { get; set; }
    }
}
