using System;
using System.Collections.Generic;
using System.Net;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using ArticlesWeb.Business.Abstract;
using ArticlesWeb.Business.Results;
using ArticlesWeb.Entities.DbEntities;
using ArticlesWeb.Entities.RequestModels;
using ArticlesWeb.Repository.Abstract;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;

namespace ArticlesWeb.Business.Concrete
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _repository;

        public UserService(IUserRepository repository)
        {
            _repository = repository;
        }

        public IResult Register(UserRegisterModel user)
        {
            User newUser = new User()
            {
                Username = user.Username,
                Password = user.Password,
                Email = user.Email,
                Fullname = user.Fullname,
                Birthday = user.BirthDate,
                PostCount = 0,
                About = user.About,
                RegistrationDate = DateTime.Now,
                isAdmin = false
            };
            try
            {
                _repository.Add(newUser);
                return new SuccessResult(Messages.RegisterSuccesful);
            }
            catch (Exception ex)
            {
                return new ErrorResult(ex.Message);
            }
        }

        public async Task<IResult> SignInAsync(UserLoginModel user, HttpContext httpContext)
        {
            var result = _repository.Get(u => u.Username == user.Username
                                 && u.Password == user.Password);

            if (result == null)
            {
                return new ErrorResult(Messages.WrongInput);
            }

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, result.Username),
                new Claim(ClaimTypes.Role, "User")
            };

            // admin ise role admin de eklenir
            if(result.isAdmin)
            {
                claims.Add(new Claim(ClaimTypes.Role, "Admin"));
            }
            var userIdentity = new ClaimsIdentity(claims, "Login");
            var principal = new ClaimsPrincipal(userIdentity);
            await httpContext.SignInAsync(principal);
            return new SuccessResult();
        }
    }
}
