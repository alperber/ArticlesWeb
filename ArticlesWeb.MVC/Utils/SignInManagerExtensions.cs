using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using ArticlesWeb.Business.Results;
using ArticlesWeb.MVC.Models.DbEntities;
using ArticlesWeb.MVC.Models.RequestModels;
using ArticlesWeb.MVC.Services;
using AutoMapper;
using Microsoft.AspNetCore.Identity;

namespace ArticlesWeb.MVC.Utils
{
    public static class SignInManagerExtensions
    {
        public static async Task<IResult> SignIn(this SignInManager<User> manager, 
            UserLoginModel user,
            IMapper mapper)
        {
            var dbUser = await manager.UserManager.FindByEmailAsync(user.Email);
            
            if (dbUser is null)
            {
                return new ErrorResult(Messages.WrongInput);
            }

            var roles = new List<Claim> { new Claim(ClaimTypes.Role, "User") };

            if (dbUser.IsAdmin) roles.Add(new Claim(ClaimTypes.Role, "Admin"));
            

            await manager.SignInWithClaimsAsync(dbUser, false, roles);
            
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