using Ambev.DeveloperEvaluation.Domain.Entities.Sale;
using Ambev.DeveloperEvaluation.Domain.Enums;

namespace Ambev.DeveloperEvaluation.Application.Sales.CreateSale
{
    public class CreateSaleResult
    {
        public Guid Id { get; set; }
        public Guid CompanyId { get; set; }
        public Guid UserId { get; set; }
        public IReadOnlyCollection<SaleDiscount>? Discounts { get; set; }
        public IReadOnlyCollection<SaleItem>? Products { get; set; }
        public SaleStatus Status { get; set; } = SaleStatus.Started;
    }
}