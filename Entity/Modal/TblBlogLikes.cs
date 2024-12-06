using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity.Modal
{
    public class TblBlogLikes
    {
        [Key]
        public int LikeId { get; set; }
        public int BlogPostId { get; set; }
        public string UserId { get; set; }
        public TblBlogPost TblBlogPost { get; set; }
    }
}
