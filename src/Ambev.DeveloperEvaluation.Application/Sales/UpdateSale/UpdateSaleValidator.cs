using FluentValidation;

namespace Ambev.DeveloperEvaluation.Application.Sales.UpdateSale
{
    public class UpdateSaleValidator : AbstractValidator<UpdateSaleCommand>
    {
        public UpdateSaleValidator()
        {
            RuleFor(s => s.Id).NotNull().NotEmpty().WithMessage("Id is mandatory");
            RuleFor(s => s.CompanyId).NotNull().NotEmpty().WithMessage("CompanyId is mandatory");
            RuleFor(s => s.Items).NotEmpty().WithMessage("For one sale is necessary products");
            RuleFor(s => s.CompanyId).NotNull().NotEmpty().WithMessage("CompanyId is mandatory");
        }
    }
}