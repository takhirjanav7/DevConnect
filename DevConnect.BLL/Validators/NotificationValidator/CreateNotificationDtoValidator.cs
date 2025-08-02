using DevConnect.BLL.DTOs.NotificationDTOs;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevConnect.BLL.Validators.NotificationValidator
{
    public class CreateNotificationDtoValidator : AbstractValidator<CreateNotificationDto>
    {
        public CreateNotificationDtoValidator()
        {
            RuleFor(x => x.RecipientId)
                .NotEmpty();

            RuleFor(x => x.Type)
                .NotEmpty().Must(t => new[] { "Follow", "Comment", "Message", "Like" }.Contains(t));

            RuleFor(x => x.Message)
                .NotEmpty().MaximumLength(200);
        }

    }
}
