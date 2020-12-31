using ArticlesWeb.MVC.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using ArticlesWeb.Business.Abstract;
using ArticlesWeb.Entities.RequestModels;
using ArticlesWeb.Repository;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;

namespace ArticlesWeb.MVC.Controllers
{
    public class HomeController : Controller
    {
        private readonly IUserService _userService;
        private readonly IPostService _postService;

        public HomeController(ILogger<HomeController> logger, IUserService userService, ArticlesContext context, IPostService postService)
        {
            _userService = userService;
            _postService = postService;
        }

        public IActionResult Index()
        {
            var response = _postService.GetAllPostsWithUser();

            if (!response.Success)
            {
                // 404 error
            }
            return View(response.Data);
        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Register(UserRegisterModel user)
        {
            var response = _userService.Register(user);

            if (response.Success)
            {
                // kayıt başarılı oldu ise login sayfasına yönlendir
                TempData["RegisterSuccessful"] = response.Message;
                return RedirectToAction(nameof(Login));
            }

            TempData["RegisterError"] = response.Message;
            return RedirectToAction(nameof(Register));
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(UserLoginModel user)
        {
            var response = await _userService.SignInAsync(user, HttpContext);
            if (response.Success)
            {
                TempData["name"] = user.Username;
                return RedirectToAction(nameof(Index));
            }

            TempData["Error"] = response.Message;
            return RedirectToAction(nameof(Login));
        }

        [Authorize]
        public async Task<IActionResult> SignOut()
        {
            await _userService.SignOutAsync(HttpContext);

            return RedirectToAction(nameof(Index));
        }

        [Authorize]
        public IActionResult EditProfile()
        {
            int userId = int.Parse(User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Sid).Value);

            var user = _userService.GetUserDetailsById(userId);

            return View(user.Data);
        }


        [Authorize]
        [HttpPost]
        public IActionResult EditProfile(UserUpdateModel user)
        {
            user.UserId = int.Parse(User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Sid).Value);

            var response = _userService.UpdateUser(user);

            if (!response.Success)
            {
                TempData["UpdateError"] = response.Message;
                return RedirectToAction(nameof(EditProfile));
            }

            // RedirectToAction with parameter
            return RedirectToAction("Details","Users" ,new
            {
                userId = user.UserId
            });
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
