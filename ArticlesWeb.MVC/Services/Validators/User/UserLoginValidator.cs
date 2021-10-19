using ArticlesWeb.MVC.Models.RequestModels;
using FluentValidation;

namespace ArticlesWeb.MVC.Services.Validators.User
{
    public class UserLoginValidator : AbstractValidator<UserLoginModel>
    {
        public UserLoginValidator()
        {
            RuleFor(user => user.Email)
                .NotEmpty()
                .NotNull()
                .EmailAddress()
                .WithMessage("Please make sure email is valid");

            RuleFor(user => user.Password)
                .NotEmpty()
                .NotNull()
                .Matches(@"^(?=.*[0-9])(?=.*[a-z])(?=.*[A-Z])(?=.*[*.!@$%^&(){}[]:;<>,.?/~_+-=|\]).{8,32}$");
        }
    }
}