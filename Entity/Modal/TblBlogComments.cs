using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity.Modal
{
    public class TblBlogComments
    {
        [Key]
        public int Id { get; set; }
        public int BlogPostId { get; set; }
        [Column(TypeName = "nvarchar(max)")]
        public string Content { get; set; }
        public string UserId { get; set; }
        public DateTime DateCreated { get; set; } = DateTime.Now;
        public TblBlogPost TblBlogPost { get; set; }
    }
}
