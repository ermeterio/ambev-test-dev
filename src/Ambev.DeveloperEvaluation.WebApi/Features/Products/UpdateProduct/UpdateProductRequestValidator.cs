using FluentValidation;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Products.UpdateProduct;

public class UpdateProductRequestValidator : BaseProductValidator<UpdateProductRequest>
{
    public UpdateProductRequestValidator()
    {
        RuleFor(product => product.Id).NotNull();
    }
}