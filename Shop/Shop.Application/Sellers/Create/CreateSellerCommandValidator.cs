using Common.Application.Validations;
using Common.Application.Validations.FluentValidations;
using FluentValidation;

namespace Shop.Application.Sellers.Create;

public class CreateSellerCommandValidator : AbstractValidator<CreateSellerCommand>
{
    public CreateSellerCommandValidator()
    {
        RuleFor(r=>r.ShopName).NotEmpty().WithMessage(ValidationMessages.required("نام فروشگاه"));

        RuleFor(r=>r.NationalCode).NotEmpty().WithMessage(ValidationMessages.required("کد ملی"))
            .ValidNationalCode();
    }

}
