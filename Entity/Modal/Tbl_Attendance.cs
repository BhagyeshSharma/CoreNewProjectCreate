using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity.Modal
{
    public class Tbl_Attendance
    {
        [Key]
        public int Attendance_Id { get; set; }
        public int StudentId { get; set; }
        public string StudentName { get; set; } 
        public DateTime Date { get; set; }
        public bool IsPresent { get; set; }
    }
}
