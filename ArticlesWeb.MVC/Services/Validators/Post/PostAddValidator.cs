using ArticlesWeb.MVC.Models.RequestModels;
using FluentValidation;

namespace ArticlesWeb.MVC.Services.Validators.Post
{
    public class PostAddValidator : AbstractValidator<PostAddModel>
    {
        public PostAddValidator()
        {
            RuleFor(model => model.Title)
                .NotEmpty()
                .NotNull()
                .WithMessage("Title Cannot be null") // TODO get this error messages from static class
                .Length(3, 50);

            RuleFor(model => model.Text)
                .NotEmpty()
                .NotNull()
                .Length(10, 1000);
        }
    }
}