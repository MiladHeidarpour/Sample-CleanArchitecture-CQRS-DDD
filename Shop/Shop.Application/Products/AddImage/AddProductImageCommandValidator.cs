using Common.Application.Validations;
using Common.Application.Validations.FluentValidations;
using FluentValidation;

namespace Shop.Application.Products.AddImage;

public class AddProductImageCommandValidator:AbstractValidator<AddProductImageCommand>
{
    public AddProductImageCommandValidator()
    {
        RuleFor(r => r.ImageFile)
            .NotNull().WithMessage(ValidationMessages.required("تصویر"))
            .JustImageFile();

        RuleFor(r => r.Sequence)
            .NotNull().NotEmpty().WithMessage(ValidationMessages.required("مکان تصویر"))
            .GreaterThanOrEqualTo(0);
    }
}
