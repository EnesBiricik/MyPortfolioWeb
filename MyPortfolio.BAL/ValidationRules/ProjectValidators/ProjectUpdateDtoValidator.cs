using FluentValidation;
using MyPortfolio.Dtos;

namespace MyPortfolio.BAL.ValidationRules.ProjectValidators
{
    public class ProjectUpdateDtoValidator : AbstractValidator<ProjectUpdateDto>
    {
        public ProjectUpdateDtoValidator()
        {
            RuleFor(x => x.Id).NotEmpty();
            RuleFor(x => x.Name).NotEmpty();
            RuleFor(x => x.Description).NotEmpty();
            RuleFor(x => x.LiveDemoLink).NotEmpty();
            RuleFor(x => x.GithubLink).NotEmpty();
        }
    }
}
