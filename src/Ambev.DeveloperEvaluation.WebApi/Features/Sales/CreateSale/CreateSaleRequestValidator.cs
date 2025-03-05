using FluentValidation;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.CreateSale;

public class CreateSaleRequestValidator : BaseSaleValidator<CreateSaleRequest>
{
    public CreateSaleRequestValidator()
    {
        RuleFor(sale => sale.UserId).NotNull().NotEmpty();
        RuleFor(sale => sale.CompanyId).NotNull().NotEmpty();
        RuleFor(sale => sale.Items).NotNull().NotEmpty();
    }
}