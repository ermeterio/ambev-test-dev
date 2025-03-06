using Ambev.DeveloperEvaluation.Domain.Entities.Product;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Ambev.DeveloperEvaluation.ORM.Repositories
{
    public class DiscountRepository : BaseRepository<Discount>, IDiscountRepository
    {
        public DiscountRepository(DefaultContext context) : base(context)
        {
        }

        public Task<Discount?> GetIdenticalDiscount(Discount entity, CancellationToken cancellationToken)
            => DbSet.FirstOrDefaultAsync(d => d.ProductId == entity.ProductId && 
                                                d.CompanyId == entity.CompanyId && 
                                                d.StartDate == entity.StartDate &&
                                                d.EndDate == entity.EndDate, cancellationToken);
    }
}