using Ambev.DeveloperEvaluation.Domain.Entities.Sale;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.GetSale;

public class GetSaleResponse
{
    public Guid CompanyId { get; set; }
    public Guid UserId { get; set; }
    public IReadOnlyCollection<SaleDiscount>? Discounts { get; set; }
    public required IReadOnlyCollection<SaleItem> Products { get; set; }
}