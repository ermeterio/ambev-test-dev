namespace Ambev.DeveloperEvaluation.Application.Products.CreateDiscount
{
    public class CreateDiscountResult
    {
        public Guid Id { get; set; }
        public Guid ProductId { get; set; }
        public decimal Value { get; set; }
        public int Quantity { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}