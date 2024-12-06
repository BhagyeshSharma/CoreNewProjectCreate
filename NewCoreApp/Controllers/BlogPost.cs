using ClassDAL;
using Data;
using Entity.ViewModal;
using InfraStucture.Contract;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Reporting.Map.WebForms.BingMaps;

namespace NewCoreApp.Controllers
{
    public class BlogPostController : Controller
    {
        private readonly UserMgMtContext _context;
        private readonly IUnitOfWork _unitOfWork;
        private readonly dbRepository _dboperations;
        private readonly IWebHostEnvironment _environment;
        //private readonly UserManager<ApplicationUser> _userManager;

        public BlogPostController(UserMgMtContext context, IUnitOfWork unitOfWork, dbRepository dbRepository, IWebHostEnvironment environment, SchoolEducationDBContext sEDContext) 
        {
            _context = context;
            _unitOfWork = unitOfWork;
            _dboperations = dbRepository;
            _environment = environment;
        }

        // List blog posts
        public async Task<IActionResult> Index()
        {
            var posts = await _context.TblBlogPost.Include(p => p.TblBlogComments).Include(p => p.TblBlogLikes).ToListAsync();
            return View(posts);
        }

        // View a single blog post with comments
        public async Task<IActionResult> ViewPost(int id)
        {
            var post = await _context.TblBlogPost
       .Where(p => p.BlogPostId == id)
       .Select(p => new BlogPostViewModel
       {
           BlogPost = p,
           Comments = _context.TblBlogComments.Where(comment => comment.BlogPostId == p.BlogPostId).ToList(),
           Likes = _context.TblBlogLikes.Where(like => like.BlogPostId == p.BlogPostId).ToList()
       })
       .FirstOrDefaultAsync();

            if (post == null)
            {
                return NotFound();
            }
            return View(post);
        }

        // Add a comment to a blog post
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddComment(int postId, string content)
        {
            //if (User.Identity.IsAuthenticated)
            //{
                //var userId = _userManager.GetUserId(User);
            var userId = "1";
            var comment = new Entity.Modal.TblBlogComments
                {
                    BlogPostId = postId,
                    Content = content,
                    UserId = userId
                };

                _context.TblBlogComments.Add(comment);
                await _context.SaveChangesAsync();
            //}
            return RedirectToAction(nameof(ViewPost), new { id = postId });
        }

        // Like a blog post
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> LikePost(int postId)
        {
            //if (User.Identity.IsAuthenticated)
            //{
            //var userId = _userManager.GetUserId(User);
            var userId = "1";
            var like = new Entity.Modal.TblBlogLikes
                {
                    BlogPostId = postId,
                    UserId = userId
                };

                _context.TblBlogLikes.Add(like);
                await _context.SaveChangesAsync();
            //}
            return RedirectToAction(nameof(ViewPost), new { id = postId });
        }
    }
}
