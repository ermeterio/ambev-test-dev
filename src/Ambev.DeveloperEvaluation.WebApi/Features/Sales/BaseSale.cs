using Ambev.DeveloperEvaluation.Domain.Entities.Sale;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales;

public class BaseSale
{
    public Guid CompanyId { get; set; }
    public Guid UserId { get; set; }
    public IReadOnlyCollection<SaleDiscount>? Discounts { get; set; }
    public IReadOnlyCollection<SaleItem> Items { get; set; } = [];
}