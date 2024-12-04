using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Entity.Modal;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entity.ViewModal
{
    public class InsertDataModel
    {
        public List<StudentEdu_Details> StudentEdu_Details { get; set; }

        public int ? StudentId { get; set; }
        [Display(Name = "Student Roll no")]
        public string StudentRollNo { get; set; }
        [Display(Name = "Student Name")]
        public string StudentName { get; set; }

        [Display(Name = "Student Email")]
        public string StudentEmail { get; set; }

        [Display(Name = "Student Mobile no")]
        public string StudentMobNo { get; set; }
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
