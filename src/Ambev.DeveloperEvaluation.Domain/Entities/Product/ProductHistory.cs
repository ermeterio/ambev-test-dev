using Ambev.DeveloperEvaluation.Domain.Common;

namespace Ambev.DeveloperEvaluation.Domain.Entities.Product
{
    public class ProductHistory : BaseEntity
    {
        public string ProductId { get; set; } = string.Empty;
        public string UserId { get; set; } = string.Empty;
        public DateTime Date { get; set; } = DateTime.UtcNow;
        public decimal Price { get; set; }
        public int QuantityRequested { get; set; }
        public int StockInitial { get; set; }
        public int StockFinal { get; set; }
    }
}