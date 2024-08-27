using Common.Application.Validations;
using FluentValidation;

namespace Shop.Application.Users.ChangePassword;

public class ChangeUserPasswordCommandValidation : AbstractValidator<ChangeUserPasswordCommand>
{
    public ChangeUserPasswordCommandValidation()
    {
        RuleFor(b => b.CurrentPassword)
            .NotEmpty().WithMessage(ValidationMessages.required("کلمه عبور فعلی"))
            .MinimumLength(5).WithMessage(ValidationMessages.required("کلمه عبور فعلی"));

        RuleFor(b => b.Password)
            .NotEmpty().WithMessage(ValidationMessages.required("کلمه عبور فعلی"))
            .MinimumLength(5).WithMessage(ValidationMessages.required("کلمه عبور فعلی"));
    }
}