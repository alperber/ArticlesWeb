using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ArticlesWeb.Business.Abstract;

namespace ArticlesWeb.MVC.Controllers
{
    public class UsersController : Controller
    {
        private readonly IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        [Route("Users/Details/{userId}")]
        public IActionResult Details(int userId)
        {
            var response = _userService.GetUserDetailsById(userId);

            if (!response.Success || response.Data == null)
            {
                return NotFound();
            }

            return View(response.Data);
        }
    }
}
