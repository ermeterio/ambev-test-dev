using FluentValidation;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Products.UpdateProduct;

public class UpdateProductRequestValidator : AbstractValidator<UpdateProductRequest>
{
    public UpdateProductRequestValidator()
    {
        RuleFor(product => product.Id).NotNull();
        RuleFor(product => product.CategoryId).NotNull();
        RuleFor(product => product.CompanyId).NotNull();
        RuleFor(product => product.Description).MaximumLength(500);
        RuleFor(product => product.Name).NotEmpty();
        RuleFor(product => product.Price).GreaterThan(0);
        RuleFor(product => product.ActualStock).GreaterThan(0);
    }
}