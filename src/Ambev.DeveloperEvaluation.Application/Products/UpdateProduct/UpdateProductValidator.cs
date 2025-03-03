using FluentValidation;

namespace Ambev.DeveloperEvaluation.Application.Products.UpdateProduct
{
    public class UpdateProductValidator : BaseProductValidator<UpdateProductCommand> 
    {
        public UpdateProductValidator()
        {
            RuleFor(p => p.Id).NotNull().NotEmpty().WithMessage("Id is mandatory");
        }
    }
}