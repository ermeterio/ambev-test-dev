﻿using FluentValidation;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Products.CreateDiscount;

public class CreateDiscountRequestValidator : AbstractValidator<CreateDiscountRequest>
{
    public CreateDiscountRequestValidator()
    {
        RuleFor(c => c.ProductId).NotNull().NotEmpty().WithMessage("ProductId is mandatory");
        RuleFor(c => c.Value).NotNull().NotEmpty().WithMessage("Value is mandatory");
        RuleFor(c => c.Quantity).GreaterThanOrEqualTo(1).WithMessage("Quantity is major for 0");
        RuleFor(c => c.StartDate).NotNull().NotEmpty().WithMessage("StartDate is mandatory");
        RuleFor(c => c.EndDate).NotNull().NotEmpty().WithMessage("EndDate is mandatory");
    }
}