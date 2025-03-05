using Ambev.DeveloperEvaluation.Domain.Common;

namespace Ambev.DeveloperEvaluation.Domain.Entities.Product
{
    public class Category : BaseEntity
    {
        public string Name { get; set; } = string.Empty;
        public IReadOnlyCollection<Product>? Products { get; set; }
    }
}