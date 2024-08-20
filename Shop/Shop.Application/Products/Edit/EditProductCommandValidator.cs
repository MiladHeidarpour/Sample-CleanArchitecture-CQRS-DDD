using Common.Application.Validations.FluentValidations;
using Common.Application.Validations;
using FluentValidation;

namespace Shop.Application.Products.Edit;

public class EditProductCommandValidator : AbstractValidator<EditProductCommand>
{
    public EditProductCommandValidator()
    {
        RuleFor(r => r.Title).NotEmpty().WithMessage(ValidationMessages.required("عنوان"));

        RuleFor(r => r.Description).NotEmpty().WithMessage(ValidationMessages.required("توضیحات"));

        RuleFor(r => r.ImageFile).JustImageFile();

        RuleFor(r => r.Slug)
           .NotEmpty().WithMessage(ValidationMessages.required("slug"));
    }
}