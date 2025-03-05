using FluentValidation;

namespace Ambev.DeveloperEvaluation.Application.Products.GetProduct
{
    public class GetProductValidator : AbstractValidator<GetProductCommand>
    {
        public GetProductValidator()
        {
             RuleFor(p => p.Id).NotEmpty().NotNull().WithMessage("Id is mandatory");
        }
    }
}