namespace Ambev.DeveloperEvaluation.Domain.Events.Product
{
    public class UpdateProductEvent
    {
        public Guid Id { get; set; }
        public required string Name { get; set; }
        public required string Description { get; set; }
        public decimal Price { get; set; }
        public int ActualStock { get; set; }
        public int NewStock { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}