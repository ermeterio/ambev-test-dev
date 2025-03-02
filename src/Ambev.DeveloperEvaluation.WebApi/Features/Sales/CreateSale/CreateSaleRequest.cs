using Ambev.DeveloperEvaluation.Domain.Entities.Sale;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.CreateSale;

public class CreateSaleRequest
{
    public Guid CompanyId { get; set; }
    public Guid UserId { get; set; }
    public IReadOnlyCollection<SaleDiscount>? Discounts { get; set; }
    public required IReadOnlyCollection<SaleItem> Products { get; set; }
}