using Ambev.DeveloperEvaluation.Domain.Common;

namespace Ambev.DeveloperEvaluation.Domain.Entities.Sale
{
    public class SaleDiscount : BaseEntity
    {
        public Guid SaleId { get; set; }
        public Sale? Sale { get; set; }
        public Guid DiscountId { get; set; }
        public Product.Discount? Discount { get; set; }
    }
}