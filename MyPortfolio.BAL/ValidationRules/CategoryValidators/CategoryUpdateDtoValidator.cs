using FluentValidation;
using MyPortfolio.Dtos;

namespace MyPortfolio.BAL.ValidationRules.CategoryValidators
{
    public class CategoryUpdateDtoValidator : AbstractValidator<CategoryUpdateDto>
    {
        public CategoryUpdateDtoValidator()
        {
            RuleFor(x => x.Id).NotEmpty();
            RuleFor(x => x.Name).NotEmpty();
        }
    }
}
