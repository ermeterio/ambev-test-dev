using FluentValidation;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Products;

public class BaseProductValidator<T> : AbstractValidator<T> where T : BaseProduct
{
    public BaseProductValidator()
    {
        RuleFor(product => product.CategoryId).NotNull().WithMessage("CategoryId is mandatory");
        RuleFor(product => product.CompanyId).NotNull().WithMessage("CompanyId is mandatory");
        RuleFor(product => product.Description).MaximumLength(500).WithMessage("Max length os description is 500 characters");
        RuleFor(product => product.Name).NotEmpty().WithMessage("Name is mandatory");
        RuleFor(product => product.Price).GreaterThan(0).WithMessage("Price GreaterThan then 0");
        RuleFor(product => product.MaxItemsForSale).GreaterThan(0).WithMessage("MaxItemsForSale GreaterThan then 0");
        RuleFor(product => product.ActualStock).GreaterThan(0).WithMessage("ActualStock GreaterThan then 0");
    }
}
