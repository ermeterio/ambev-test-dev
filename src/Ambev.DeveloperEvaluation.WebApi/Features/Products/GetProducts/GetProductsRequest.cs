using Microsoft.AspNetCore.Mvc;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Products.GetProducts;

public class GetProductsRequest
{
    [FromQuery]
    public string Name { get; set; } = string.Empty;
    [FromQuery]
    public Guid CategoryId { get; set; } = default;
    [FromQuery]
    public int CurrentPage { get; set; } = 1;
    [FromQuery]
    public int PageSize { get; set; } = 30;
}