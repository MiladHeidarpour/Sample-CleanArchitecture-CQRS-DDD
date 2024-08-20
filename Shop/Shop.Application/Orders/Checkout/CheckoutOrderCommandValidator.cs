using Common.Application.Validations;
using Common.Application.Validations.FluentValidations;
using FluentValidation;

namespace Shop.Application.Orders.Checkout;

public class CheckoutOrderCommandValidator : AbstractValidator<CheckoutOrderCommand>
{
    public CheckoutOrderCommandValidator()
    {
        RuleFor(r => r.Name)
            .NotEmpty()
            .NotNull().WithMessage(ValidationMessages.required("نام"));

        RuleFor(r => r.Family)
            .NotEmpty()
            .NotNull().WithMessage(ValidationMessages.required("نام خانوادگی"));

        RuleFor(r => r.Shire)
            .NotEmpty()
            .NotNull().WithMessage(ValidationMessages.required("استان"));

        RuleFor(r => r.City)
            .NotEmpty()
            .NotNull().WithMessage(ValidationMessages.required("شهر"));

        RuleFor(r => r.PhoneNumber)
            .NotEmpty()
            .NotNull().WithMessage(ValidationMessages.required("شماره تلفن"))
            .MaximumLength(11).WithMessage("شماره تلفن نامعتبر است")
            .MinimumLength(11).WithMessage("شماره تلفن نامعتبر است");

        RuleFor(r => r.NationalCode)
            .NotEmpty()
            .NotNull().WithMessage(ValidationMessages.required("کد ملی"))
            .MaximumLength(10).WithMessage("کد ملی نامعتبر است")
            .MinimumLength(10).WithMessage("کد ملی نامعتبر است")
            .ValidNationalCode();

        RuleFor(r => r.PostalAddress)
            .NotEmpty()
            .NotNull().WithMessage(ValidationMessages.required("آدرس پستی"));

        RuleFor(r => r.PostalCode)
            .NotEmpty()
            .NotNull().WithMessage(ValidationMessages.required("کد پستی"));
    }

}
