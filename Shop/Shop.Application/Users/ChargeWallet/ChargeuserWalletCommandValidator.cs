using Common.Application.Validations;
using FluentValidation;

namespace Shop.Application.Users.ChargeWallet;

public class ChargeuserWalletCommandValidator:AbstractValidator<ChargeuserWalletCommand>
{
    public ChargeuserWalletCommandValidator()
    {
        RuleFor(r => r.Description)
            .NotEmpty().WithMessage(ValidationMessages.required("توضیحات"));

        RuleFor(r => r.Price)
            .GreaterThanOrEqualTo(1000);
    }
}
