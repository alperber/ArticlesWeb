using System;
using System.Collections.Generic;
using System.Text;
using ArticlesWeb.Business.Results;
using ArticlesWeb.Entities.RequestModels;

namespace ArticlesWeb.Business.Abstract
{
    public interface IUserService
    {
        IResult Register(UserRegisterModel user);
    }
}
