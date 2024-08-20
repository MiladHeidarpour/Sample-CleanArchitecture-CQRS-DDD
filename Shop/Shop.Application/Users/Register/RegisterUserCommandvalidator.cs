using Common.Application.Validations;
using FluentValidation;

namespace Shop.Application.Users.Register;

public class RegisterUserCommandvalidator:AbstractValidator<RegisterUserCommand>
{
    public RegisterUserCommandvalidator()
    {
        RuleFor(r => r.Password)
            .NotEmpty().WithMessage(ValidationMessages.required("کلمه عبور"))
            .NotNull().MinimumLength(4).WithMessage("کلمه عبور باید بیشتر از 4 کارکتر باشد");
    }
}