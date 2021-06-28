using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using ArticlesWeb.Business.Abstract;
using ArticlesWeb.Entities.RequestModels;
using Microsoft.AspNetCore.Authorization;
using Newtonsoft.Json;

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

        [Route("/Posts/Details/{postId}")]
        public IActionResult Details(string postId)
        {
            var response = _postService.GetPostById(postId);
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
            post.UserId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Sid).Value;
            _postService.AddPost(post);
            return RedirectToAction("Index", "Home");
        }


        // client-side'da ajax ile erişilecek
        [Authorize]
        [HttpPost]
        public IActionResult AddComment(CommentAddModel comment)
        {
            comment.UserId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Sid).Value;

            var response = _commentService.AddComment(comment);

            if (response.Success)
                return RedirectToAction("Details", "Posts", new
                {
                    postId = comment.PostId
                });

            return RedirectToAction("Index", "Home");
        }
    }
}
