using FluentValidation;

namespace Ambev.DeveloperEvaluation.Application.Products
{
    public class BaseProductValidator<T> : AbstractValidator<T> where T : BaseProduct
    {
        public BaseProductValidator()
        {
            RuleFor(p => p.CompanyId).NotNull().NotEmpty().WithMessage("CompanyId is mandatory");
            RuleFor(p => p.Name).NotNull().NotEmpty().WithMessage("Name is mandatory");
            RuleFor(p => p.Price).NotNull().NotEmpty().WithMessage("Price is mandatory");
            RuleFor(p => p.CategoryId).NotNull().NotEmpty().WithMessage("CategoryId is mandatory");
            RuleFor(p => p.ActualStock).NotNull().NotEmpty().WithMessage("ActualStock is mandatory");
        }
    }
}