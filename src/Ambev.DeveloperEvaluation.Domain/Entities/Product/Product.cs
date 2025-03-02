using Ambev.DeveloperEvaluation.Domain.Common;
using Ambev.DeveloperEvaluation.Domain.Events;

namespace Ambev.DeveloperEvaluation.Domain.Entities.Product
{
    public class Product : BaseEntity, IEntityWithEvents
    {
        public required string Name { get; set; }
        public decimal Price { get; set; }
        public Guid CategoryId { get; set; }
        public Category? Category { get; set; }
        public ProductImage? Image { get; set; } 
        public string Description { get; set; } = string.Empty;
        public int ActualStock { get; set; }
        public Guid CompanyId { get; set; }
        public Company.Company? Company { get; set; }
    }
}