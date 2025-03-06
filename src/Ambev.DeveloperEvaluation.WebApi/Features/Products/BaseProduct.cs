using Ambev.DeveloperEvaluation.Domain.Entities.Product;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Products;

public class BaseProduct
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public Guid CategoryId { get; set; }
    public ProductImage? Image { get; set; }
    public string Description { get; set; } = string.Empty;
    public int ActualStock { get; set; }
    public Guid CompanyId { get; set; }
    public int MaxItemsForSale { get; set; }
}
