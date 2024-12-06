using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity.Modal
{
    public class TblBlogPost
    {
        [Key]
        public int BlogPostId { get; set; }
        [Required]
        [Column(TypeName = "nvarchar(max)")]
        public string Title { get; set; }
        [Required]
        [Column(TypeName = "nvarchar(max)")]
        public string Content { get; set; }
        public DateTime DateCreated { get; set; } = DateTime.Now;
        public ICollection<TblBlogComments> TblBlogComments { get; set; }
        public ICollection<TblBlogLikes> TblBlogLikes { get; set; }
    }
}
