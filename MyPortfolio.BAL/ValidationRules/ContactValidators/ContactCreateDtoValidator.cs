using FluentValidation;
using MyPortfolio.Dtos;

namespace MyPortfolio.BAL.ValidationRules.ContactValidators
{
    public class ContactCreateDtoValidator : AbstractValidator<ContactCreateDto>
    {
        public ContactCreateDtoValidator()
        {
            RuleFor(x => x.Name).NotEmpty();
            RuleFor(x => x.EmailAddress).NotEmpty();
            RuleFor(x => x.Subject).NotEmpty();
            RuleFor(x => x.Message).NotEmpty();
        }
    }
}
