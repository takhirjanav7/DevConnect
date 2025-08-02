using DevConnect.BLL.DTOs.CommentDTOs;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevConnect.BLL.Validators.CommentValidator
{
    public class CreateCommentDtoValidator : AbstractValidator<CreateCommentDto>
    {
        public CreateCommentDtoValidator()
        {
            RuleFor(x => x.Text)
                .NotEmpty().MaximumLength(300);

            RuleFor(x => x.PostId)
                .NotEmpty().WithMessage("PostId bo‘sh bo‘lmasligi kerak");
        }

    }
}
