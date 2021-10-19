using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using ArticlesWeb.MVC.Models.RequestModels;
using ArticlesWeb.MVC.Utils;
using Microsoft.AspNetCore.Authorization;
using ArticlesWeb.MVC.Services.Abstract;

namespace ArticlesWeb.MVC.Controllers
{
    public class PostsController : Controller
    {
        private readonly IPostService _postService;
        private readonly ICommentService _commentService;

        public PostsController(IPostService postService, ICommentService commentService)
        {
            _postService = postService;
            _commentService = commentService;
        }

        [Route("/Posts/{postId}/Details")]
        public async Task<IActionResult> Details(Guid postId)
        {
            var response = await _postService.GetPostById(postId);
            
            if (response.Data == null || !response.Success)
            {
                return RedirectToAction("Index", "Home");
            }
            return View(response.Data);
        }

        [Authorize]
        public IActionResult AddPost()
        {
            return View();
        }

        [Authorize]
        [HttpPost]
        public IActionResult AddPost(PostAddModel post)
        {
            post.UserId = User.Claims.GetUserId();
            _postService.AddPost(post);
            return RedirectToAction("Index", "Home");
        }

        
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> AddComment(CommentAddModel comment)
        {
            comment.UserId = User.Claims.GetUserId();

            var response = await _commentService.AddComment(comment);

            if (response.Success)
            {
                return RedirectToAction("Details", "Posts", new
                {
                    postId = comment.PostId
                });
            }
            return RedirectToAction("Index", "Home");
        }
    }
}
