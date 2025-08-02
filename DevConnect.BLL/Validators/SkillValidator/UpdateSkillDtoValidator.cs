using DevConnect.BLL.DTOs.SkillDTOs;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevConnect.BLL.Validators.SkillValidator
{
    public class UpdateSkillDtoValidator : AbstractValidator<UpdateSkillDto>
    {
        public UpdateSkillDtoValidator()
        {
            RuleFor(x => x.Name)
            .MaximumLength(50)
            .Matches("^[a-zA-Z0-9 +#-]+$")
            .When(x => !string.IsNullOrWhiteSpace(x.Name));

            RuleFor(x => x.Level)
                .Must(level => new[] { "Beginner", "Intermediate", "Advanced", "Expert" }.Contains(level))
                .When(x => !string.IsNullOrWhiteSpace(x.Level));

        }
    }
}
