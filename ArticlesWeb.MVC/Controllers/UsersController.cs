using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using ArticlesWeb.MVC.Utils;
using Microsoft.AspNetCore.Identity;
using ArticlesWeb.MVC.Models.DbEntities;
using AutoMapper;

namespace ArticlesWeb.MVC.Controllers
{
    public class UsersController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly IMapper _mapper;

        public UsersController(UserManager<User> userManager, IMapper mapper)
        {
            _userManager = userManager;
            _mapper = mapper;
        }

        [Route("Users/{userId}/Details")]
        public async Task<IActionResult> Details(Guid userId)
        {
            var response = await _userManager.GetUserDetailsById(userId, _mapper);

            return View(response);
        }
    }
}
