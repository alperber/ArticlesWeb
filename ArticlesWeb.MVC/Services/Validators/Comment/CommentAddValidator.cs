using ArticlesWeb.MVC.Models.RequestModels;
using FluentValidation;

namespace ArticlesWeb.MVC.Services.Validators.Comment
{
    public class CommentAddValidator : AbstractValidator<CommentAddModel>
    {
        public CommentAddValidator()
        {
            RuleFor(comment => comment.Text)
                .NotEmpty()
                .NotNull()
                .Length(3, 50);

            RuleFor(comment => comment.PostId)
                .NotEmpty()
                .NotNull();
            
            RuleFor(comment => comment.UserId)
                .NotEmpty()
                .NotNull();
        }
    }
}