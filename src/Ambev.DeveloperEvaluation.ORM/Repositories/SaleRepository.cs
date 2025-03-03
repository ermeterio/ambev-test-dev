using Ambev.DeveloperEvaluation.Domain.Entities.Product;
using Ambev.DeveloperEvaluation.Domain.Entities.Sale;
using Ambev.DeveloperEvaluation.Domain.Enums;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Ambev.DeveloperEvaluation.ORM.Repositories
{
    public class SaleRepository : BaseRepository<Sale>, ISaleRepository
    {
        public SaleRepository(DefaultContext context) : base(context)
        { }

        public Task<Sale?> GetExistingSaleForUser(Guid requestUserId, CancellationToken cancellationToken)
            => DbSet.FirstOrDefaultAsync(s => s.UserId == requestUserId && 
                                              s.Status != SaleStatus.Finished && 
                                              s.Status != SaleStatus.Canceled, cancellationToken);

        public async Task RemoveAllDiscountsForSale(Guid saleId, CancellationToken cancellationToken)
        {
            var sales = Context.ProductDiscounts?.Where(s => s.Id == saleId);
            if (sales is not null && sales.Any())
            {
                Context.ProductDiscounts!.RemoveRange(sales);
                await Context.SaveChangesAsync(cancellationToken);
            }
        }

        public async Task AddDiscountsForSale(IEnumerable<SaleDiscount> discounts, CancellationToken cancellationToken)
        {
            await Context.SaleDiscounts!.AddRangeAsync(discounts, cancellationToken);
            await Context.SaveChangesAsync(cancellationToken);
        }
    }
}