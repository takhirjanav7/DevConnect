using DevConnect.BLL.DTOs.SkillDTOs;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevConnect.BLL.Validators.SkillValidator
{
    public class CreateSkillDtoValidator : AbstractValidator<CreateSkillDto>
    {
        public CreateSkillDtoValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().MaximumLength(50);

            RuleFor(x => x.Level)
                .NotEmpty().Must(l => new[] { "Beginner", "Intermediate", "Advanced" }.Contains(l));
        }

    }
}
