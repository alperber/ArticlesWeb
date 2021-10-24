using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Threading.Tasks;
using ArticlesWeb.MVC.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using ArticlesWeb.MVC.Services.Abstract;
using ArticlesWeb.MVC.Models.DbEntities;
using ArticlesWeb.MVC.Models.RequestModels;
using AutoMapper;

namespace ArticlesWeb.MVC.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        private readonly IPostService _postService;
        private readonly ICommentService _commentService;
        private readonly UserManager<User> _userManager;
        private readonly IMapper _mapper;

        public AdminController(IPostService postService, 
            ICommentService commentService,
            UserManager<User> userManager, 
            IMapper mapper)
        {
            _postService = postService;
            _commentService = commentService;
            _userManager = userManager;
            _mapper = mapper;
        }

        public IActionResult Index()
        {
            return RedirectToAction(nameof(Users));
        }

        public async Task<IActionResult> Users()
        {
            var response = await _userManager.GetAllUsers();
            return View(response);
        }
        public async Task<IActionResult> Posts()
        {
            var response = await _postService.GetAllPostsWithUser();

            return View(response.Data);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteComment([Bind("postId, commentId")] Guid postId, Guid commentId)
        {
            var response = await _commentService.DeleteComment(commentId);

            TempData["Message"] = response.Message;

            return RedirectToAction("Details", "Posts", new
            {
                postId
            });
        }

        [Route("/Admin/DeleteUser/{userId}")]
        public async Task<IActionResult> DeleteUser([FromRoute]Guid userId)
        {
            var user = await _userManager.FindByIdAsync(userId.ToString());
            var response = await _userManager.DeleteAsync(user);

            TempData["Message"] = response.Errors.First().Description;

            return RedirectToAction(nameof(Users));
        }

        [Route("/Admin/DeletePost/{postId}")]
        public async Task<IActionResult> DeletePost(Guid postId)
        {
            var response = await _postService.DeletePostById(postId);

            TempData["Message"] = response.Message;

            return RedirectToAction(nameof(Posts));
        }

        [HttpGet]
        [Route("/Admin/UpdateUsersProfile/{userId}")]
        public async Task<IActionResult> UpdateUsersProfile(Guid userId)
        {
            var response = await _userManager.GetUserDetailsById(userId, _mapper);
            
            return View(response);
        }
        
        [HttpPost]
        [Route("/Admin/UpdateUsersProfile/{userId:guid}")]
        public async Task<IActionResult> UpdateUsersProfile([FromRoute]Guid userId, [FromForm] UserUpdateModel model)
        {
            var updatedUser = _mapper.Map<User>(model);

            var response = await _userManager.UpdateAsync(updatedUser);

            if (!response.Succeeded)
            {
                TempData["UpdateError"] = response.Errors.First().ToString();
                return RedirectToAction(nameof(UpdateUsersProfile));
            }
            
            return RedirectToAction(nameof(UpdateUsersProfile), "Admin", new
            {
                userId
            });
        }
    }
}
