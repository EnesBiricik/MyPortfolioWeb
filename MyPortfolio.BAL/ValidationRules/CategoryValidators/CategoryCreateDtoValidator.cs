using FluentValidation;
using MyPortfolio.Dtos;

namespace MyPortfolio.BAL.ValidationRules.CategoryValidators
{
    public class CategoryCreateDtoValidator : AbstractValidator<CategoryCreateDto>
    {
        public CategoryCreateDtoValidator()
        {
            RuleFor(x => x.Name).NotEmpty();
        }
    }
}
