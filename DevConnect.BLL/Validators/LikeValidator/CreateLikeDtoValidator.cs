using DevConnect.BLL.DTOs.LikeDTOs;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevConnect.BLL.Validators.LikeValidator
{
    public class CreateLikeDtoValidator: AbstractValidator<CreateLikeDto>
    {
        public CreateLikeDtoValidator() 
        {
            RuleFor(x => x.PostId)
                .NotEmpty()
                .WithMessage("PostId bo‘sh bo‘lmasligi kerak");
        }
    }
}
