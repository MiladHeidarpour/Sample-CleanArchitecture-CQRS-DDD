using Common.Application.Validations;
using FluentValidation;

namespace Shop.Application.Categories.Create;

public class CreateCategoryCommandValidator:AbstractValidator<CreateCategoryCommand>
{
    public CreateCategoryCommandValidator()
    {
        RuleFor(r => r.Title)
           .NotNull().NotEmpty().WithMessage(ValidationMessages.required("عنوان"));

        RuleFor(r => r.Slug)
          .NotNull().NotEmpty().WithMessage(ValidationMessages.required("اسلاگ"));
    }
}
