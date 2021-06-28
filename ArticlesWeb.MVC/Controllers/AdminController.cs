﻿using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ArticlesWeb.Business.Abstract;
using Microsoft.AspNetCore.Authorization;

namespace ArticlesWeb.MVC.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        private readonly IUserService _userService;
        private readonly IPostService _postService;
        private readonly ICommentService _commentService;

        public AdminController(IUserService userService, IPostService postService, ICommentService commentService)
        {
            _userService = userService;
            _postService = postService;
            _commentService = commentService;
        }

        public IActionResult Index()
        {
            return RedirectToAction(nameof(Users));
        }

        public IActionResult Users()
        {
            var response = _userService.GetAllUsers();
            return View(response.Data);
        }
        public IActionResult Posts()
        {
            var response = _postService.GetAllPostsWithUser();

            return View(response.Data);
        }

        [HttpPost]
        public IActionResult DeleteComment([Bind("postId, commentId")] string postId, string commentId)
        {
            var response = _commentService.DeleteComment(commentId);

            TempData["Message"] = response.Message;

            return RedirectToAction("Details", "Posts", new
            {
                postId = postId
            });
        }

        [Route("/Admin/DeleteUser/{userId}")]
        public IActionResult DeleteUser(string userId)
        {
            var response = _userService.DeleteUserById(userId);

            TempData["Message"] = response.Message;

            return RedirectToAction(nameof(Users));
        }

        [Route("/Admin/DeletePost/{postId}")]
        public IActionResult DeletePost(string postId)
        {
            var response = _postService.DeletePostById(postId);

            TempData["Message"] = response.Message;

            return RedirectToAction(nameof(Posts));
        }

    }
}
