using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity.Modal
{
    public class Tbl_BookTransaction
    {
        [Key]
        public int BookTransactionId { get; set; } 
        public int StudentId { get; set; }
        public int BookId { get; set; }
        public DateTime BorrowedDate { get; set; }
        public DateTime ? ReturnDate { get; set; }
        public double PenaltyAmount { get; set; } // For overdue penalties
    }
}
