using Ambev.DeveloperEvaluation.Domain.Common;

namespace Ambev.DeveloperEvaluation.Domain.Entities.Product
{
    public class ProductImage : BaseEntity
    {
        public Guid ProductId { get; set; }
        public string Image { get; set; } = string.Empty;
        public int Order { get; set; }
        public bool IsMain { get; set; }
    }
}