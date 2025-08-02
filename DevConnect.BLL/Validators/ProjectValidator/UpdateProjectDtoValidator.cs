using DevConnect.BLL.DTOs.ProjectDTOs;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevConnect.BLL.Validators.ProjectValidator;

public class UpdateProjectDtoValidator : AbstractValidator<UpdateProjectDto>
{
    public UpdateProjectDtoValidator()
    {
        RuleFor(x => x.Title)
            .MaximumLength(100)
            .When(x => !string.IsNullOrWhiteSpace(x.Title));

        RuleFor(x => x.Description)
            .MaximumLength(500)
            .When(x => !string.IsNullOrWhiteSpace(x.Description));

        RuleFor(x => x.ImageUrl)
            .Must(url => Uri.IsWellFormedUriString(url, UriKind.Absolute))
            .When(x => !string.IsNullOrWhiteSpace(x.ImageUrl));

        RuleFor(x => x.RepositoryUrl)
            .Must(url => Uri.IsWellFormedUriString(url, UriKind.Absolute))
            .When(x => !string.IsNullOrWhiteSpace(x.RepositoryUrl));
    }
}
