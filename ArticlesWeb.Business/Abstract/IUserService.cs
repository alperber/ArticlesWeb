﻿using System;
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
        IDataResult<User> GetUserDetailsById(string userId);
        IDataResult<User> GetUserDetailsByUsername(string username);
        IResult DeleteUserById(string userId);
        IResult IncrementPostCount(string userId);
        IResult DecrementPostCount(string userId);
        IResult UpdateUser(UserUpdateModel user);
        IResult CheckIfEmailExists(string email);
        IResult CheckIfUsernameExists(string username);
        IDataResult<List<User>> GetAllUsers();
    }
}
