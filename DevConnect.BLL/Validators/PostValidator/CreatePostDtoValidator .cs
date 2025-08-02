using DevConnect.BLL.DTOs.PostDTOs;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevConnect.BLL.Validators.PostValidator
{
    public class CreatePostDtoValidator : AbstractValidator<CreatePostDto>
    {
        public CreatePostDtoValidator() 
        {
            RuleFor(p => p.Title)
                .NotEmpty()
                .MaximumLength(500);


            RuleFor(p => p.Description)
                .NotEmpty()
                .MaximumLength(1000);
        }
    }
}
