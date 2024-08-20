using Common.Application.Validations;
using FluentValidation;

namespace Shop.Application.Comments.Create;

public class CreateCommentCommandValidator : AbstractValidator<CreateCommentCommand>
{
    public CreateCommentCommandValidator()
    {
        RuleFor(r => r.Text)
            .NotNull().NotEmpty()
            .MinimumLength(5).WithMessage(ValidationMessages.minLength("متن نظر", 5));
    }
}

