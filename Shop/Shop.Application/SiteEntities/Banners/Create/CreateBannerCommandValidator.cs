using Common.Application.Validations;
using Common.Application.Validations.FluentValidations;
using FluentValidation;

namespace Shop.Application.SiteEntities.Banners.Create;

public class CreateBannerCommandValidator : AbstractValidator<CreateBannerCommand>
{
    public CreateBannerCommandValidator()
    {
        RuleFor(r => r.ImageFile)
            .NotNull().WithMessage(ValidationMessages.required("تصویر"))
            .JustImageFile();

        RuleFor(r=>r.Link)
           .NotNull()
           .NotEmpty().WithMessage(ValidationMessages.required("لینک"));
    }
}
