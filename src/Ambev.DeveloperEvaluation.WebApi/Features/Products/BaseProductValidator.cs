using FluentValidation;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Products;

public class BaseProductValidator<T> : AbstractValidator<T> where T : BaseProduct
{
    public BaseProductValidator()
    {
        RuleFor(product => product.CategoryId).NotNull();
        RuleFor(product => product.CompanyId).NotNull();
        RuleFor(product => product.Description).MaximumLength(500);
        RuleFor(product => product.Name).NotEmpty();
        RuleFor(product => product.Price).GreaterThan(0);
        RuleFor(product => product.ActualStock).GreaterThan(0);
    }
}
