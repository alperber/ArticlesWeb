using ArticlesWeb.MVC.Models.RequestModels;
using FluentValidation;

namespace ArticlesWeb.MVC.Services.Validators.User
{
    public class UserRegisterValidator: AbstractValidator<UserRegisterModel>
    {
        public UserRegisterValidator()
        {
            RuleFor(user => user.Email)
                .EmailAddress()
                .WithMessage("Please make sure email is valid")
                .NotEmpty()
                .NotNull();

            RuleFor(user => user.About)
                .Length(1, 50);

            RuleFor(user => user.Password)
                .NotEmpty()
                .NotNull()
                .Matches(@"^(?=.*[0-9])(?=.*[a-z])(?=.*[A-Z])(?=.*[*.!@$%^&(){}[]:;<>,.?/~_+-=|\]).{8,32}$");

            RuleFor(user => user.FirstName)
                .NotEmpty()
                .NotNull()
                .Length(3, 50);
            
            RuleFor(user => user.LastName)
                .NotEmpty()
                .NotNull()
                .Length(3, 50);
            
            RuleFor(user => user.UserName)
                .NotEmpty()
                .NotNull()
                .Length(3, 50);
        }
    }
}