using Ambev.DeveloperEvaluation.Domain.Entities.Sale;
using Ambev.DeveloperEvaluation.Domain.Enums;

namespace Ambev.DeveloperEvaluation.Application.Sales.GetSale
{
    public class GetSaleResult
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public string Code { get; set; } = string.Empty;
        public List<SaleDiscount>? Discounts { get; set; } = [];
        public List<SaleItem>? Items { get; set; } = [];
        public SaleStatus Status { get; set; }
    }
}