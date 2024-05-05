using FluentValidation;
using MyPortfolio.Dtos;

namespace MyPortfolio.BAL.ValidationRules.PageSettingsValidators
{
    public class PageSettingsUpdateDtoValidator : AbstractValidator<PageSettingsUpdateDto>
    {
        public PageSettingsUpdateDtoValidator()
        {
            RuleFor(x => x.Id).NotEmpty();
            RuleFor(x => x.Name).NotEmpty().WithMessage("AD-SOYAD alanı boş bırakılamaz");
            RuleFor(x => x.Email).NotEmpty().WithMessage("Email alanı boş bırakılamaz");
            RuleFor(x => x.SloganSentence).NotEmpty().WithMessage("Slogan alanı boş bırakılamaz");
            RuleFor(x => x.AboutDescription).NotEmpty().WithMessage("Hakkında Açıklaması alanı boş bırakılamaz");
            RuleFor(x => x.Job).NotEmpty().WithMessage("Hakkında Açıklaması alanı boş bırakılamaz");
            RuleFor(x => x.ShortDescription).NotEmpty().WithMessage("Hakkında Açıklaması alanı boş bırakılamaz");

        }

    }
}
