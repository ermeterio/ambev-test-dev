using FluentValidation;

namespace Ambev.DeveloperEvaluation.Application.Products.DeleteDiscount
{
    public class DeleteDiscountValidator : AbstractValidator<DeleteDiscountCommand>
    {
        public DeleteDiscountValidator()
        {
            RuleFor(d => d.Id).NotNull().NotEmpty().WithMessage("Id is mandatory");
        }
    }
}