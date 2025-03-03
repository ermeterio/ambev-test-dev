using FluentValidation;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales;

public class BaseSaleValidator<T> : AbstractValidator<T> where T : BaseSale
{
    public BaseSaleValidator()
    {
        RuleFor(sale => sale.UserId).NotNull().NotEmpty();
        RuleFor(sale => sale.CompanyId).NotNull().NotEmpty();
        RuleFor(sale => sale.Items).NotNull().NotEmpty();
    }
}