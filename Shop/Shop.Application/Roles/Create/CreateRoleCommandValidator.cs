using Common.Application.Validations;
using FluentValidation;

namespace Shop.Application.Roles.Create;

public class CreateRoleCommandValidator:AbstractValidator<CreateRoleCommand>
{
    public CreateRoleCommandValidator()
    {
        RuleFor(r => r.Title)
            .NotEmpty()
            .WithMessage(ValidationMessages.required("عنوان"));
    }
}