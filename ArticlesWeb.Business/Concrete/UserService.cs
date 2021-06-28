using System;
using System.Collections.Generic;
using System.Net;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using ArticlesWeb.Business.Abstract;
using ArticlesWeb.Business.Helpers;
using ArticlesWeb.Business.Results;
using ArticlesWeb.Entities.DbEntities;
using ArticlesWeb.Entities.RequestModels;
using ArticlesWeb.Repository.Abstract;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace ArticlesWeb.Business.Concrete
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _repository;
        private readonly IServiceProvider _serviceProvider;

        public UserService(IUserRepository repository, IServiceProvider serviceProvider)
        {
            _repository = repository;
            _serviceProvider = serviceProvider;
        }

        public IResult Register(UserRegisterModel user)
        {
            IResult check = CheckIfUsernameExists(user.Username);
            if (check.Success) return new ErrorResult(check.Message);

            check = CheckIfEmailExists(user.Email);
            if (check.Success) return new ErrorResult(check.Message);

            User newUser = UserHelper.CreateUser(user);
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
                new Claim(ClaimTypes.Sid, result.UserId.ToString()),
                new Claim(ClaimTypes.Name, result.Username),
                new Claim(ClaimTypes.Role, "User")
            };

            if(result.IsAdmin)
            {
                claims.Add(new Claim(ClaimTypes.Role, "Admin"));
            }
            var userIdentity = new ClaimsIdentity(claims, "Login");
            var principal = new ClaimsPrincipal(userIdentity);
            await httpContext.SignInAsync(principal);

            return new SuccessResult();
        }

        public async Task<IResult> SignOutAsync(HttpContext httpContext)
        {
            try
            {
                await httpContext.SignOutAsync();
                return new SuccessResult();
            }
            catch (Exception e)
            {
                return new ErrorResult(e.Message);
            }
        }

        public IDataResult<User> GetUserDetailsById(string userId)
        {
            return new SuccessDataResult<User>(_repository.Get(u => u.UserId == userId));
        }

        public IDataResult<User> GetUserDetailsByUsername(string username)
        {
            return new SuccessDataResult<User>(_repository.Get(u => u.Username == username));
        }

        public IResult DeleteUserById(string userId)
        {
            var user = _repository.Get(u => u.UserId == userId);

            if (user == null)
            {
                return new ErrorResult(Messages.UserDoesntExists);
            }

            //IPostService postService = _serviceProvider.GetRequiredService<IPostService>();
            //var response = postService.DeleteUserOwnedPosts(userId);

            //if (!response.Success)
            //    return new ErrorResult(response.Message);

            try
            {
                //var commentService = _serviceProvider.GetRequiredService<ICommentService>();
                //commentService.DeleteUserComments(user.UserId);
                _repository.Delete(user);
                return new SuccessResult(Messages.UserDeleted);
            }
            catch (Exception e)
            {
                return new ErrorResult(e.Message);
            }
        }

        public IResult IncrementPostCount(string userId)
        {
            _repository.IncrementPostCount(userId);

            return new SuccessResult();
        }

        public IResult DecrementPostCount(string userId)
        {
            _repository.DecrementPostCount(userId);
            
            return new SuccessResult();
        }

        public IResult UpdateUser(UserUpdateModel user)
        {
            User oldUser = _repository.Get(u => u.UserId == user.UserId);

            if (oldUser.Username != user.Username)
            {
                IResult check1 = CheckIfUsernameExists(user.Username);
                if (check1.Success) return new ErrorResult(check1.Message);
            }

            if (oldUser.Email != user.Email)
            {
                IResult check2 = CheckIfEmailExists(user.Email);
                if (check2.Success) return new ErrorResult(check2.Message);
            }

            User newUser = UserHelper.UpdatedUser(user, oldUser);

            try
            {
                _repository.Update(newUser);
                return new SuccessResult();
            }
            catch (Exception e)
            {
                return new ErrorResult(e.Message);
            }
        }

        public IResult CheckIfEmailExists(string email)
        {
            var user = _repository.Get(u => u.Email == email);
            if (user == null)
            {
                return new ErrorResult();
            }
            return new SuccessResult(Messages.EmailAlreadyExists);
        }

        public IResult CheckIfUsernameExists(string username)
        {
            var user = _repository.Get(u => u.Username == username);
            if (user == null)
            {
                return new ErrorResult();
            }
            return new SuccessResult(Messages.UsernameAlreadyExists);
        }

        public IDataResult<List<User>> GetAllUsers()
        {
            return new SuccessDataResult<List<User>>(_repository.GetList());
        }
    }
}
