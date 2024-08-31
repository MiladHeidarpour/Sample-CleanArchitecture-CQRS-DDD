﻿using Common.Application.Validations.FluentValidations;
using FluentValidation;

namespace Shop.Application.Users.Edit;

public class EditUserCommandValidator:AbstractValidator<EditUserCommand>
{
    public EditUserCommandValidator()
    {
        RuleFor(r => r.PhoneNumber)
            .ValidPhoneNumber();

        RuleFor(r => r.Email)
            .EmailAddress().WithMessage("ایمیل نامعتبر است");

        //RuleFor(r => r.Password)
        //    .MinimumLength(4).WithMessage("کلمه عبور باید بیشتر از 4 کارکتر باشد");

        RuleFor(r => r.Avatar)
            .JustImageFile();
    }
}
