using ArticlesWeb.MVC.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using ArticlesWeb.Business.Abstract;
using ArticlesWeb.Entities.RequestModels;

namespace ArticlesWeb.MVC.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IUserService _userService;

        public HomeController(ILogger<HomeController> logger, IUserService userService)
        {
            _logger = logger;
            _userService = userService;
        }

        public IActionResult Index()
        {
            _logger.LogInformation("GET /Home/Index");
            return View();
        }

        public IActionResult Register()
        {
            _logger.LogInformation("GET /Home/Register");
            return View();
        }

        [HttpPost]
        public IActionResult Register(UserRegisterModel user)
        {
            _logger.LogInformation("POST /Home/Register");
            var response = _userService.Register(user);

            if (response.Success)
            {
                // kayıt başarılı oldu ise login sayfasına yönlendir
                TempData["name"] = user.Fullname;
                return RedirectToAction(nameof(Login));
            }
            return RedirectToAction(nameof(Register));
        }

        public IActionResult Login()
        {
            _logger.LogInformation("GET /Home/Login");
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            _logger.LogInformation("GET /Home/Error");
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
