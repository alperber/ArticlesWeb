using System;
using System.Collections.Generic;
using System.Text;
using ArticlesWeb.Business.Abstract;
using ArticlesWeb.Business.Results;
using ArticlesWeb.Entities.DbEntities;
using ArticlesWeb.Entities.RequestModels;
using ArticlesWeb.Repository.Abstract;

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
    }
}
