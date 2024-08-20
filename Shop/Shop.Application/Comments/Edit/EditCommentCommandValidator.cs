using Common.Application.Validations;
using FluentValidation;

namespace Shop.Application.Comments.Edit;

public class EditCommentCommandValidator : AbstractValidator<EditCommentCommand>
{
    public EditCommentCommandValidator()
    {
        RuleFor(r => r.Text)
            .NotNull().NotEmpty()
            .MinimumLength(5).WithMessage(ValidationMessages.minLength("متن نظر", 5));
    }
}
