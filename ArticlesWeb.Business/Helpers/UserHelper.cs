using System;
using System.Collections.Generic;
using System.Text;
using ArticlesWeb.Entities.DbEntities;
using ArticlesWeb.Entities.RequestModels;

namespace ArticlesWeb.Business.Helpers
{
    public static class UserHelper
    {
        public static User CreateUser(UserRegisterModel user)
        {
            return new User()
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
        }
    }
}
