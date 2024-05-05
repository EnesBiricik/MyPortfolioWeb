using FluentValidation;
using MyPortfolio.Dtos;

namespace MyPortfolio.BAL.ValidationRules.BlogValidators
{
    public class BlogCreateDtoValidator : AbstractValidator<BlogCreateDto>
    {
        public BlogCreateDtoValidator()
        {
            RuleFor(x => x.Name).NotEmpty();
            RuleFor(x => x.CategoryId).NotEmpty();
            RuleFor(x => x.Description).NotEmpty();
            RuleFor(x => x.Content).NotEmpty();
        }
    }
}
