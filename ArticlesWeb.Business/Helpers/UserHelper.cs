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
            var (hashedP, salt) = PasswordHasher.CreatePasswordHash(user.Password);
            
            return new User()
            {
                Username = user.Username,
                Password = hashedP,
                PasswordSalt = salt,
                Email = user.Email,
                Fullname = user.Fullname,
                Birthday = user.BirthDate,
                PostCount = 0,
                About = user.About,
                RegistrationDate = DateTime.Now,
                IsAdmin = false
            };
        }
        public static User UpdatedUser(UserUpdateModel user, User oldUser)
        {
            return new User
            {
                Username = user.Username,
                About = user.About,
                Birthday = user.Birthday,
                Email = user.Email,
                Fullname = user.Fullname,
                Password = user.Password,
                PostCount = oldUser.PostCount,
                RegistrationDate = oldUser.RegistrationDate,
                UserId = oldUser.UserId,
                IsAdmin = oldUser.IsAdmin
            };
        }
    }
}
