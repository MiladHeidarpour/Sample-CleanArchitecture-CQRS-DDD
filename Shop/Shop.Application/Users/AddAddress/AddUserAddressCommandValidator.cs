using Common.Application.Validations;
using Common.Application.Validations.FluentValidations;
using FluentValidation;

namespace Shop.Application.Users.AddAddress;

public class AddUserAddressCommandValidator:AbstractValidator<AddUserAddressCommand>
{
    public AddUserAddressCommandValidator()
    {
        RuleFor(r => r.City)
            .NotEmpty().WithMessage(ValidationMessages.required("شهر"));

        RuleFor(r => r.Shire)
            .NotEmpty().WithMessage(ValidationMessages.required("استان"));

        RuleFor(r => r.Name)
            .NotEmpty().WithMessage(ValidationMessages.required("نام"));

        RuleFor(r => r.Family)
            .NotEmpty().WithMessage(ValidationMessages.required("نام خانوادگی"));

        RuleFor(r => r.NationalCode)
            .NotEmpty().WithMessage(ValidationMessages.required("کد ملی"))
            .ValidNationalCode();

        RuleFor(r => r.PostalAddress)
            .NotEmpty().WithMessage(ValidationMessages.required("آدرس پستی"));

        RuleFor(r => r.PostalCode)
            .NotEmpty().WithMessage(ValidationMessages.required("کد پستی"));
    }
}
