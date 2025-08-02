using DevConnect.BLL.DTOs.CommentDTOs;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevConnect.BLL.Validators.CommentValidator
{
    public class UpdateCommentDtoValidator : AbstractValidator<UpdateCommentDto>
    {
        public UpdateCommentDtoValidator()
        {
            RuleFor(x => x.Text)
                .MaximumLength(300)
                .When(x => !string.IsNullOrWhiteSpace(x.Text));
        }
    }
}
