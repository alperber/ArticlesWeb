using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using ArticlesWeb.Business.Abstract;
using ArticlesWeb.Entities.RequestModels;
using Microsoft.AspNetCore.Authorization;

namespace ArticlesWeb.MVC.Controllers
{
    public class PostsController : Controller
    {
        private readonly IPostService _postService;

        public PostsController(IPostService postService)
        {
            _postService = postService;
        }

        [Route("/Posts/Details/{postId}")]
        public IActionResult Details(int postId)
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
            post.UserId = int.Parse(User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Sid).Value);
            _postService.AddPost(post);
            return RedirectToAction("Index", "Home");
        }

        // yorum ekleme
        public IActionResult AddComment()
        {
            return View();
        }
    }
}
