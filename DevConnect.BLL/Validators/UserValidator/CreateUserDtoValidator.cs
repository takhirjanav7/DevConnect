using DevConnect.BLL.DTOs.UserDTOs;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace DevConnect.BLL.Validators.UserValidator;

public class CreateUserDtoValidator : AbstractValidator<CreateUserDto>
{
    public CreateUserDtoValidator()
    {
        RuleFor(u => u.Username)
            .NotEmpty()
            .Length(3, 50)
            .WithMessage("Username majburiy.");


        RuleFor(u => u.Email)
            .NotEmpty()
            .WithMessage("Email kiritilishi kerak")
            .EmailAddress()
            .WithMessage("Email formati noto‘g‘ri.");

        RuleFor(u => u.Password)
            .NotEmpty()
            .WithMessage("Parol kiritilishi kerak.")
            .Must(ConfirmPassword)
            .WithMessage("Parol kamida 6 ta belgidan iborat bo'lishi, kamida bitta katta harf, bitta kichik harf, bitta raqam va bitta maxsus belgi (@!?#$%^&*()) bo'lishi kerak.");


        RuleFor(u => u.Role)
            .NotEmpty()
            .WithMessage("Role kiritilishi kerak.")
            .Must(role => new[] { "Developer", "Recruiter", "Learner" }.Contains(role))
            .WithMessage("Role noto'g'ri: Developer, Recruiter yoki Learner bo'lishi kerak.");

    }
    private bool ConfirmPassword( string password)
    {
        string res = @"^(?=.*[A-Z])(?=.*[a-z])(?=.*\d)(?=.*[@!?#\$%\^&\*\(\)]).{6,}$";
        var result = Regex.IsMatch(password, res);

        return result;
    }
}
