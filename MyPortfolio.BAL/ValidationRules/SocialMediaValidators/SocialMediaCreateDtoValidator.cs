using FluentValidation;
using MyPortfolio.Dtos;

namespace MyPortfolio.BAL.ValidationRules.SocialMediaValidators
{
    public class SocialMediaCreateDtoValidator : AbstractValidator<SocialMediaCreateDto>
    {
        public SocialMediaCreateDtoValidator()
        {
            RuleFor(x => x.Name).NotEmpty();
            RuleFor(x => x.Link).NotEmpty();
        }
    }
}
