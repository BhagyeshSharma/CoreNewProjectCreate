using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity.Modal
{
    public class Tbl_Books
    {
        [Key]
        public int Book_Id { get; set; }
        [Display(Name ="Book Title")]
        [StringLength(150)]
        [Column(TypeName = "nvarchar(150)")]
        public string BookTitle { get; set; }
        [Display(Name = "Author")]
        [StringLength(50)]
        [Column(TypeName = "nvarchar(50)")]
        public string BookAuthor { get; set; }
        public bool IsAvailable { get; set; }  // To track availability
        public string Barcode { get; set; }  // Barcode for the book
    }
}
