using FluentValidation;
using MyPortfolio.Dtos;

namespace MyPortfolio.BAL.ValidationRules.BlogValidators
{
    public class BlogUpdateDtoValidator : AbstractValidator<BlogUpdateDto>
    {
        public BlogUpdateDtoValidator()
        {
            RuleFor(x => x.Id).NotEmpty();
            RuleFor(x => x.Name).NotEmpty();
            RuleFor(x => x.CategoryId).NotEmpty();
            RuleFor(x => x.Description).NotEmpty();
            RuleFor(x => x.Content).NotEmpty();
        }
    }
}
