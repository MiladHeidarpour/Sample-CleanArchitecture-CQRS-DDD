using Common.Application.Validations;
using Common.Application.Validations.FluentValidations;
using FluentValidation;

namespace Shop.Application.SiteEntities.Sliders.Create;

public class CreateSliderCommandValidator:AbstractValidator<CreateSliderCommand>
{
    public CreateSliderCommandValidator()
    {
        RuleFor(r => r.ImageFile)
            .NotNull().WithMessage(ValidationMessages.required("تصویر"))
            .JustImageFile();

        RuleFor(r => r.Link)
           .NotNull()
           .NotEmpty().WithMessage(ValidationMessages.required("لینک"));

        RuleFor(r => r.Title)
           .NotNull()
           .NotEmpty().WithMessage(ValidationMessages.required("عنوان"));
    }
}
