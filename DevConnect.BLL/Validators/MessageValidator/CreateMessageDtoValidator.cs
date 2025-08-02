using DevConnect.BLL.DTOs.MessageDTOs;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevConnect.BLL.Validators.MessageValidator
{
    public class CreateMessageDtoValidator : AbstractValidator<CreateMessageDto>
    {
        public CreateMessageDtoValidator()
        {
            RuleFor(x => x.ReceiverId)
                .NotEmpty().WithMessage("Qabul qiluvchi UserId majburiy");

            RuleFor(x => x.Text)
                .NotEmpty().MaximumLength(300);
        }
    }
}
