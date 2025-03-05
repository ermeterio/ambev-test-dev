namespace Ambev.DeveloperEvaluation.Domain.Events.Product
{
    public class CreateProductEvent 
    {
        public Guid Id { get; set; }
        public required string Name { get; set; }
        public required string Description { get; set; }
        public decimal Price { get; set; }
        public int Stock { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}