using Entity.Modal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity.ViewModal
{
    public class ExamSessionViewModal
    {
        public int SessionId { get; set; } // Unique session ID for the user
        public List<TblQuestion> TblQuestion { get; set; } // List of questions for the exam
        public int TimeLimit { get; set; } // Time limit in seconds   
        public DateTime StartTime { get; set; }
    }
}
