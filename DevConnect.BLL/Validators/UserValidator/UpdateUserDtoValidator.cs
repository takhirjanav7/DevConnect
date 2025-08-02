using DevConnect.BLL.DTOs.UserDTOs;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevConnect.BLL.Validators.UserValidator
{
    public class UpdateUserDtoValidator : AbstractValidator<UpdateUserDto>
    {
        public UpdateUserDtoValidator()
        {
            RuleFor(x => x.Username)
                .MaximumLength(50).WithMessage("Username must not exceed 50 characters.")
                .MinimumLength(3).When(x => !string.IsNullOrWhiteSpace(x.Username));

            RuleFor(x => x.FullName)
                .MaximumLength(100).When(x => !string.IsNullOrWhiteSpace(x.FullName));

            RuleFor(x => x.ProfileImageUrl)
                .Must(url => Uri.TryCreate(url, UriKind.Absolute, out _))
                .When(x => !string.IsNullOrWhiteSpace(x.ProfileImageUrl))
                .WithMessage("ProfileImageUrl must be a properly formatted URL");

            RuleFor(x => x.Role)
                .Must(role => new[] { "Developer", "Recruiter", "Learner" }.Contains(role))
                .When(x => !string.IsNullOrWhiteSpace(x.Role))
                .WithMessage("Invalid role: Must be Developer, Recruiter, or Learner");
        }

    }
}
