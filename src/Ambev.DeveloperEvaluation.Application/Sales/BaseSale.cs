using Ambev.DeveloperEvaluation.Domain.Entities.Sale;
using Ambev.DeveloperEvaluation.Domain.Enums;

namespace Ambev.DeveloperEvaluation.Application.Sales
{
    public class BaseSale
    {
        public Guid CompanyId { get; set; }
        public Guid UserId { get; set; }
        public IEnumerable<SaleDiscount>? Discounts { get; set; }
        public IEnumerable<SaleItem>? Items { get; set; }
        public SaleStatus Status { get; set; }
        public bool Success { get; set; }
    }
}