using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity.Modal
{
    public class TblUserActivityLogs
    {
        [Key]
        public int ActivitylogId { get; set; }
        public int SessionId { get; set; }
        [StringLength(50)]
        [Column(TypeName = "nvarchar(50)")]
        public string EventType{ get; set; }    
        public DateTime Timestamp { get; set; } 
    }
}
