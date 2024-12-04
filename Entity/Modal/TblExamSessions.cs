using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity.Modal
{
    public class TblExamSessions
    {
        [Key]
        public int ExamSessionId {  get; set; }
        [StringLength(255)]
        [Column(TypeName = "nvarchar(255)")]
        public string UserId { get; set; } 
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public Boolean IsCompleted { get; set; }
       
    }
}
