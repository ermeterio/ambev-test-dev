namespace Ambev.DeveloperEvaluation.WebApi.Features.Products.CreateDiscount;

public class CreateDiscountRequest
{
    public Guid ProductId { get; set; }
    public decimal Value { get; set; }
    public int Quantity { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
}