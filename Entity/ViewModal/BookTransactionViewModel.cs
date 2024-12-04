using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity.ViewModal
{
    public class BookTransactionViewModel
    {
        public int Book_Id { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public bool IsAvailable { get; set; }
        public int? TransactionId { get; set; }  // Nullable in case the book is available (no transaction)
        public DateTime? BorrowedDate { get; set; }
        public DateTime? ReturnDate { get; set; }
        public double PenaltyAmount { get; set; }  // For overdue penalties, initially 0
        public int StudentId { get; set; } // Assuming the student ID will be passed along
    }
}
