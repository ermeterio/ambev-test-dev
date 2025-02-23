using Ambev.DeveloperEvaluation.Domain.Common;

namespace Ambev.DeveloperEvaluation.Domain.Entities.Product
{
    public class Discount : BaseEntity
    {
        public Guid ProductId { get; set; }
        public Product? Product { get; set; }
        public decimal Value { get; set; }
        public int Quantity { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}