using DevConnect.BLL.DTOs.FollowDTOs;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevConnect.BLL.Validators.FollowValidator
{
    public class CreateFollowDtoValidator : AbstractValidator<CreateFollowDto>
    {
        public CreateFollowDtoValidator()
        {
            RuleFor(x => x.FollowingId)
                .NotEmpty().WithMessage("Follow qilinadigan userId bo‘lishi kerak");
        }

    }
}
