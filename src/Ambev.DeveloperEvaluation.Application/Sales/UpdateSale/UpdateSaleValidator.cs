using FluentValidation;

namespace Ambev.DeveloperEvaluation.Application.Sales.UpdateSale
{
    public class UpdateSaleValidator : BaseSaleValidator<UpdateSaleCommand>
    {
        public UpdateSaleValidator()
        {
            RuleFor(s => s.Id).NotNull().NotEmpty().WithMessage("Id is mandatory");
        }
    }
}