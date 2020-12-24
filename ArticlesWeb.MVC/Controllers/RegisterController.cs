using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ArticlesWeb.Business.Abstract;
using ArticlesWeb.Entities.RequestModels;
using Microsoft.AspNetCore.Mvc;

namespace ArticlesWeb.MVC.Controllers
{
    public class RegisterController: Controller
    {
        private readonly IUserService _userService;
        public RegisterController(IUserService userService)
        {
            _userService = userService;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Register(UserRegisterModel user)
        {
            var response = _userService.Register(user);
            return View(response);
        }
    }
}
