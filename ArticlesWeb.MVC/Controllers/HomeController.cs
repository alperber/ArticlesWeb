using ArticlesWeb.MVC.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using ArticlesWeb.MVC.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Localization;
using Microsoft.Extensions.Localization;
using ArticlesWeb.MVC.Models.DbEntities;
using ArticlesWeb.MVC.Models.RequestModels;
using ArticlesWeb.MVC.Services.Abstract;
using AutoMapper;

namespace ArticlesWeb.MVC.Controllers
{
    public class HomeController : Controller
    {
        private readonly SignInManager<User> _signInManager;
        private readonly UserManager<User> _userManager;
        private readonly IPostService _postService;
        private readonly IStringLocalizer<HomeController> _localizer;
        private readonly IMapper _mapper;

        public HomeController(
            IPostService postService, 
            IStringLocalizer<HomeController> localizer, 
            SignInManager<User> signInManager, UserManager<User> userManager, IMapper mapper)
        {
            _postService = postService;
            _localizer = localizer;
            _signInManager = signInManager;
            _userManager = userManager;
            _mapper = mapper;
        }

        public async Task<IActionResult> Index()
        {
            var response = await _postService.GetAllPostsWithUser();

            return View(response.Data);
        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(UserRegisterModel user)
        {
            if (!ModelState.IsValid)
                return View(user);

            var response = await _signInManager.RegisterAsync(user);

            if (response.Success)
            {
                TempData["RegisterSuccessful"] = response.Message;
                return RedirectToAction(nameof(Index));
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
            if (!ModelState.IsValid)
            {
                return View(user);
            }
            var response = await _signInManager.SignInWithPasswordAsync(user);
            if (response.Success)
            {
                return RedirectToAction(nameof(Index));
            }

            TempData["Error"] = response.Message;
            return RedirectToAction(nameof(Login));
        }

        [Authorize]
        public async Task<IActionResult> LogOut()
        {
            await _signInManager.SignOutAsync();

            return RedirectToAction(nameof(Index));
        }

        [Authorize]
        public async Task<IActionResult> EditProfile()
        {
            var userId = User.Claims.GetUserId();

            var user = await _userManager.GetUserDetailsById(userId, _mapper);

            return View(user);
        }


        [Authorize]
        [HttpPost]
        public async Task<IActionResult> EditProfile(UserUpdateModel user)
        {
            user.Id = User.Claims.GetUserId();

            // TODO map userdto to user 
            // TODO create extension method for update user
            // TODO will update method change all properties????
            var mappedUser = new User { };
            var response = await _userManager.UpdateAsync(mappedUser);
            var t = await _userManager.ChangePasswordAsync(mappedUser, user.Password, " ");

            if (!response.Succeeded)
            {
                TempData["UpdateError"] = response.Errors.First().Description;
                return RedirectToAction(nameof(EditProfile));
            }
            
            return RedirectToAction("Details","Users" ,new
            {
                userId = user.Id
            });
        }

        [HttpPost]
        public IActionResult ChangeCulture(string culture, string returnUrl)
        {
            Response.Cookies.Append(CookieRequestCultureProvider.DefaultCookieName,
                CookieRequestCultureProvider.MakeCookieValue(new RequestCulture(culture)),
                new CookieOptions
                {
                    Expires = DateTimeOffset.Now.AddDays(30)
                });
            return LocalRedirect(returnUrl);
        }

        [ResponseCache(Duration = 60, Location = ResponseCacheLocation.Client, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
