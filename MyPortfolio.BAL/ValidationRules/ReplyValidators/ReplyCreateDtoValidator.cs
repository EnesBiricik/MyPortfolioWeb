using FluentValidation;
using MyPortfolio.Dtos;

namespace MyPortfolio.BAL.ValidationRules.ReplyValidators
{
    public class ReplyCreateDtoValidator : AbstractValidator<ReplyCreateDto>
    {
        public ReplyCreateDtoValidator()
        {
            RuleFor(x => x.AuthorName).NotEmpty();
            RuleFor(x => x.AuthorName).MaximumLength(50);
            RuleFor(x => x.CommentText).NotEmpty();
            RuleFor(x => x.AuthorEmailAddress).NotEmpty();
            RuleFor(x => x.CommentId).NotEmpty();
        }
    }
}
