using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using ArticlesWeb.Business.Results;
using ArticlesWeb.Entities.DbEntities;
using ArticlesWeb.Entities.RequestModels;
using Microsoft.AspNetCore.Http;

namespace ArticlesWeb.Business.Abstract
{
    public interface IUserService
    {
        IResult Register(UserRegisterModel user);
        Task<IResult> SignInAsync(UserLoginModel user, HttpContext httpContext);
        Task<IResult> SignOutAsync(HttpContext httpContext);
        IDataResult<User> GetUserDetailsById(int userId);
        IDataResult<User> GetUserDetailsByUsername(string username);
        IResult DeleteUserById(int userId);
        IResult IncrementPostCount(int userId);
    }
}
