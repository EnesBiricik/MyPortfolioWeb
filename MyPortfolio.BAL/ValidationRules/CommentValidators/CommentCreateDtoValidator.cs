using FluentValidation;
using MyPortfolio.Dtos;

namespace MyPortfolio.BAL.ValidationRules.CommentValidators
{
    public class CommentCreateDtoValidator : AbstractValidator<CommentCreateDto>
    {
        public CommentCreateDtoValidator()
        {
            RuleFor(x => x.AuthorName).NotEmpty();
            RuleFor(x => x.AuthorName).MaximumLength(50);
            RuleFor(x => x.CommentText).NotEmpty();
            RuleFor(x => x.AuthorEmailAddress).NotEmpty();
            RuleFor(x => x.BlogId).NotEmpty();
        }

    }
}
