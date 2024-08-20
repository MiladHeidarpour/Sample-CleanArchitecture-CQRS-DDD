﻿using Common.Application.Validations;
using Common.Application.Validations.FluentValidations;
using FluentValidation;

namespace Shop.Application.SiteEntities.Banners.Edit;

public class EditBannerCommandValidator : AbstractValidator<EditBannerCommand>
{
    public EditBannerCommandValidator()
    {
        RuleFor(r => r.ImageFile)
            .JustImageFile();

        RuleFor(r => r.Link)
           .NotNull()
           .NotEmpty().WithMessage(ValidationMessages.required("لینک"));
    }
}