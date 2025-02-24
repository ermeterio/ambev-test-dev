using Ambev.DeveloperEvaluation.Domain.Common;

namespace Ambev.DeveloperEvaluation.Domain.Entities.Product
{
    public class ProductHistory : BaseEntity
    {
        public Guid ProductId { get; set; }
        public Guid UserId { get; set; }
        public DateTime Date { get; set; }
        public decimal Price { get; set; }
        public int QuantityRequested { get; set; }
        public int StockInitial { get; set; }
        public int StockFinal { get; set; }
    }
}