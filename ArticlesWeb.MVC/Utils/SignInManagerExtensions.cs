using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using ArticlesWeb.Business.Results;
using ArticlesWeb.MVC.Models.DbEntities;
using ArticlesWeb.MVC.Models.RequestModels;
using Microsoft.AspNetCore.Identity;

namespace ArticlesWeb.MVC.Utils
{
    public static class SignInManagerExtensions
    {
        public static async Task<IResult> SignInWithPasswordAsync(this SignInManager<User> manager, UserLoginModel user)
        {
            await Task.Delay(1000);
            // check user if exist 
            
            // check admin for claim
            
            // signin with claims
            return new SuccessResult();
        }
        public static async Task<IResult> RegisterAsync(this SignInManager<User> manager, UserRegisterModel model)
        {
            var user = new User
            {
                Email = model.Email,
                About = model.About,
                BirthDate = model.BirthDate,
                FirstName = model.FirstName,
                LastName = model.LastName,
                UserName = model.UserName
            };

            var result = await manager.UserManager.CreateAsync(user, model.Password);
            if (!result.Succeeded) return new ErrorResult(result.Errors.First().Description);

            await manager.SignInAsync(user, false);
            return new SuccessResult();
        }
    }
}