using FluentValidation;
using MyPortfolio.Dtos;

namespace MyPortfolio.BAL.ValidationRules.SocialMediaValidators
{
    public class SocialMediaUpdateDtoValidator : AbstractValidator<SocialMediaUpdateDto>
    {
        public SocialMediaUpdateDtoValidator()
        {
            RuleFor(x => x.Id).NotEmpty();
            RuleFor(x => x.Name).NotEmpty().WithMessage("Sosyal Medya İsim alanı boş bırakılamaz!");
            RuleFor(x => x.Link).NotEmpty().WithMessage("Lütfen Sosyal Medya aracı için Link giriniz!");
        }
    }
}
