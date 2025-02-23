using Ambev.DeveloperEvaluation.Domain.Common;

namespace Ambev.DeveloperEvaluation.Domain.Entities.Product
{
    public class Category : BaseEntity
    {
        public required string Name { get; set; }
        public IReadOnlyCollection<Product>? Products { get; set; }
    }
}