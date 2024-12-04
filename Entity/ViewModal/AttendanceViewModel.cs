using Entity.Modal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity.ViewModal
{
    public class AttendanceViewModel
    {
        public DateTime SelectedDate { get; set; }
        public List<Tbl_Attendance> Tbl_Attendance { get; set; } 
    }
}
