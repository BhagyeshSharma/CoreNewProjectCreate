using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity.Modal
{
    public class TblQuestion
    {
        [Key]
        public int QuestionId { get; set; }
        [StringLength(500)]
        [Column(TypeName ="nvarchar(max)")]
        public string QuestionText { get; set; }
        [StringLength(255)]
        [Column(TypeName = "nvarchar(255)")]
        public string OptionA { get; set; }
        [StringLength(255)]
        [Column(TypeName = "nvarchar(255)")]
        public string OptionB { get; set; }
        [StringLength(255)]
        [Column(TypeName = "nvarchar(255)")]
        public string OptionC { get; set; }
        [StringLength(255)]
        [Column(TypeName = "nvarchar(255)")]
        public string OptionD { get; set; }
        [StringLength(50)]
        [Column(TypeName = "nvarchar(50)")]
        public string CorrectOption { get; set; }
        public int DifficultyLevel { get; set; }
        [StringLength(100)]
        [Column(TypeName = "nvarchar(100)")]
        public string Subject { get; set; }
    }
}
