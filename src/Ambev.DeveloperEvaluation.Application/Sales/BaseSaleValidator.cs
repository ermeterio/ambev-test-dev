using FluentValidation;

namespace Ambev.DeveloperEvaluation.Application.Sales
{
    public class BaseSaleValidator<T> : AbstractValidator<T> where T : BaseSale
    {
        public BaseSaleValidator()
        {
            RuleFor(s => s.CompanyId).NotNull().NotEmpty().WithMessage("CompanyId is mandatory");
            RuleFor(s => s.Items).NotEmpty().WithMessage("For one sale is necessary products");
            RuleFor(s => s.CompanyId).NotNull().NotEmpty().WithMessage("CompanyId is mandatory");
        }
    }
}