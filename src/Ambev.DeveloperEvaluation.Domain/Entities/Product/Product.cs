using Ambev.DeveloperEvaluation.Domain.Common;

namespace Ambev.DeveloperEvaluation.Domain.Entities.Product
{
    public class Product : BaseEntity
    {
        public required string Name { get; set; }
        public decimal Price { get; set; }
        public Guid CategoryId { get; set; }
        public Category? Category { get; set; }
        public string Image { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
    }
}