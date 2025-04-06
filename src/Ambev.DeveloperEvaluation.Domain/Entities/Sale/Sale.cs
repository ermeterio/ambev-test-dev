using Ambev.DeveloperEvaluation.Domain.Common;
using Ambev.DeveloperEvaluation.Domain.Enums;

namespace Ambev.DeveloperEvaluation.Domain.Entities.Sale
{
    public class Sale : BaseEntity
    {
        public Company.Company? Company { get; set; }
        public Guid UserId { get; set; }
        public User.User? User { get; set; }
        public string Code => Id.ToString()[..6].ToUpper();
        public IEnumerable<SaleDiscount>? Discounts { get; set; }
        public IEnumerable<SaleItem>? Items { get; set; }
        public SaleStatus Status { get; set; }

        public void Update(Sale sale)
        {
            if (CompanyId != sale.CompanyId)
                CompanyId = sale.CompanyId;
            if (UserId != sale.UserId)
                UserId = sale.UserId;
            if (Discounts != sale.Discounts)
                Discounts = sale.Discounts;
            if (Items != sale.Items)
                Items = sale.Items;
            if (Status != sale.Status)
                Status = sale.Status;
        }

        public bool IsValid()
        {
            if (Items == null || !Items.Any())
                return false;
            
            if(User is null)
                return false;

            if(Company is null)
                return false;

            return true;
        }

        public void Cancel()
        {
            Status = SaleStatus.Canceled;
        }
    }
}