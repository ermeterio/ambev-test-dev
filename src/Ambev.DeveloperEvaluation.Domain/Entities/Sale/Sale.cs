using Ambev.DeveloperEvaluation.Domain.Common;
using Ambev.DeveloperEvaluation.Domain.Enums;

namespace Ambev.DeveloperEvaluation.Domain.Entities.Sale
{
    public class Sale : BaseEntity
    {
        public Guid CompanyId { get; set; }
        public Company.Company? Company { get; set; }
        public Guid UserId { get; set; }
        public User.User? User { get; set; }
        public string Code => GenerateShortGuid();
        public IReadOnlyCollection<SaleDiscount>? Discounts { get; set; }
        public IReadOnlyCollection<SaleItem>? Products { get; set; }
        public SaleStatus Status { get; set; }
    }
}