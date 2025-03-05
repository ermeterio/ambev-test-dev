using Ambev.DeveloperEvaluation.Domain.Enums;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales;

public class SaleResponseBase
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public string Code { get; set; } = string.Empty;
    public List<SaleDiscountResponse>? Discounts { get; set; } = [];
    public List<SaleItemResponse>? Items { get; set; } = [];
    public SaleStatus Status { get; set; }
}
public class SaleItemResponse
{
    public Guid ProductId { get; set; }
    public ProductResponse? Product { get; set; }
    public Guid SaleId { get; set; }
    public int Quantity { get; set; }
}

public class ProductResponse
{
    public string Name { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public string Description { get; set; } = string.Empty;
}

public class SaleDiscountResponse
{
    public Guid SaleId { get; set; }
    public Guid DiscountId { get; set; }
    public DiscountResponse? Discount { get; set; }
}

public class DiscountResponse
{
    public Guid ProductId { get; set; }
    public decimal Value { get; set; }
    public int Quantity { get; set; }
}