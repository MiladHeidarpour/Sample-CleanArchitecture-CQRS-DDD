using Common.Application.Validations;
using Common.Application.Validations.FluentValidations;
using FluentValidation;

namespace Shop.Application.Products.Create;

public class CreateProductCommandValidator : AbstractValidator<CreateProductCommand>
{
    public CreateProductCommandValidator()
    {
         RuleFor(r=>r.Title).NotEmpty().WithMessage(ValidationMessages.required("عنوان"));

         RuleFor(r=>r.Description).NotEmpty().WithMessage(ValidationMessages.required("توضیحات"));

         RuleFor(r=>r.ImageFile).JustImageFile();

         RuleFor(r=>r.Slug)
            .NotEmpty().WithMessage(ValidationMessages.required("slug"));
    }
}