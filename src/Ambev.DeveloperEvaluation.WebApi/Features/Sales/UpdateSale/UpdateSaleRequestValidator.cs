using Ambev.DeveloperEvaluation.WebApi.Features.Base.Get;
using FluentValidation;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.UpdateSale;

public class UpdateSaleRequestValidator : AbstractValidator<UpdateSaleRequest>
{
    public UpdateSaleRequestValidator()
    {
        RuleFor(sale => sale.Id).NotNull().NotEmpty();
        RuleFor(sale => sale.UserId).NotNull().NotEmpty();
        RuleFor(sale => sale.CompanyId).NotNull().NotEmpty();
        RuleFor(sale => sale.Products).NotNull().NotEmpty();
    }
}