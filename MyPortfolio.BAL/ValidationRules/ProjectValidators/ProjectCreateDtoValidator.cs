using FluentValidation;
using MyPortfolio.Dtos;

namespace MyPortfolio.BAL.ValidationRules.ProjectValidators
{
    public class ProjectCreateDtoValidator : AbstractValidator<ProjectCreateDto>
    {
        public ProjectCreateDtoValidator()
        {
            RuleFor(x => x.Name).NotEmpty();
            RuleFor(x => x.Description).NotEmpty();
            RuleFor(x => x.LiveDemoLink).NotEmpty();
            RuleFor(x => x.GithubLink).NotEmpty();
        }
    }
}
