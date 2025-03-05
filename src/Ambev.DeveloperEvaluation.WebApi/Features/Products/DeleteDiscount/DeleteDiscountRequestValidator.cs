using FluentValidation;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Products.DeleteDiscount;

public class DeleteDiscountRequestValidator : AbstractValidator<DeleteDiscountRequest>
{
    public DeleteDiscountRequestValidator()
    {
        RuleFor(d => d.Id).NotNull().NotEmpty().WithMessage("Id is mandatory");
    }
}