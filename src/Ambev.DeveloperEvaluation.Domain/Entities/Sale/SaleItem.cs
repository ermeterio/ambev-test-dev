using Ambev.DeveloperEvaluation.Domain.Common;

namespace Ambev.DeveloperEvaluation.Domain.Entities.Sale
{
    public class SaleItem : BaseEntity
    {
        public Guid ProductId { get; set; }
        public Product.Product? Product { get; set; }
        public Guid SaleId { get; set; }
        public Sale? Sale { get; set; }
        public int Quantity { get; set; }
    }
}