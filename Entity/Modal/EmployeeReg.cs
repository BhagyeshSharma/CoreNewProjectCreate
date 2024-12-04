using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Net.Mime;

namespace Entity.Modal
{
    public class EmployeeReg
    {
        [Key]
        public int EmployeeId { get; set; }
        [Required]
        [Display(Name = "Employee Name")]
        [StringLength(80)]
        [Column(TypeName ="nvarchar")]
        public string EmployeeName { get; set; }
        [Required]
        [Display(Name = "Employee Father Name")]
        [StringLength(80)]
        [Column(TypeName = "nvarchar")]
        public string EmpFatherName { get; set; }
        [Required]
        public int StateId { get; set; }
        [StringLength(250)]
        [Column(TypeName = "nvarchar")]
        public string FileUpload {  get; set; } 
        [NotMapped]
        public string varfile { get; set; } 
    }
}
