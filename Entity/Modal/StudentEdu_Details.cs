using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity.Modal
{
    public class StudentEdu_Details
    {
        [Key]
        public int StudentEdu_Id { get; set; }

        public int StudentId { get; set; }

        public int SubjectId { get; set; }
        [Display(Name = "Student Class")]
        [StringLength(40)] // Increased length for emails
        [Column(TypeName = "nvarchar(40)")]
        public string StdClass { get; set; }
        [Display(Name = "Student Marks in ( % )")]
        [Column(TypeName = "decimal(18,2)")]
       public decimal StdMarks { get; set; }    
    }
}
