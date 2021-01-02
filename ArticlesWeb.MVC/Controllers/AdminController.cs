using Microsoft.AspNetCore.Mvc;
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

        [Route("/Admin/MakeAdmin/{userId}")]
        public IActionResult MakeAdmin(int userId)
        {
            var response = _userService.MakeUserAdmin(userId);

            TempData["Message"] = response.Message;
            
            return RedirectToAction(nameof(Users));
        }

        [HttpPost]
        public IActionResult DeleteComment([Bind("postId, commentId")] int postId, int commentId)
        {
            var response = _commentService.DeleteComment(commentId);

            TempData["Message"] = response.Message;

            return RedirectToAction("Details", "Posts", new
            {
                postId = postId
            });
        }

        [Route("/Admin/DeleteUser/{userId}")]
        public IActionResult DeleteUser(int userId)
        {
            var response = _userService.DeleteUserById(userId);

            TempData["Message"] = response.Message;

            return RedirectToAction(nameof(Users));
        }
    }
}
