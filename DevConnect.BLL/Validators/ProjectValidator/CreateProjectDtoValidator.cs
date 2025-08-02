using DevConnect.BLL.DTOs.ProjectDTOs;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevConnect.BLL.Validators.ProjectValidator
{
    public class CreateProjectDtoValidator : AbstractValidator<CreateProjectDto>
    {
        public CreateProjectDtoValidator()
        {
            RuleFor(x => x.Title)
                .NotEmpty().MaximumLength(100);

            RuleFor(x => x.Description)
                .NotEmpty().MaximumLength(500);
        }

    }
}
