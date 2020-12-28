using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using ArticlesWeb.Business.Results;
using ArticlesWeb.Entities.RequestModels;
using Microsoft.AspNetCore.Http;

namespace ArticlesWeb.Business.Abstract
{
    public interface IUserService
    {
        IResult Register(UserRegisterModel user);
        Task<IResult> SignInAsync(UserLoginModel user, HttpContext httpContext);
    }
}
