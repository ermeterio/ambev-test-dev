using Ambev.DeveloperEvaluation.Domain.Entities.Sale;

namespace Ambev.DeveloperEvaluation.Domain.Repositories
{
    public interface ISaleRepository : IRepository<Sale>
    {
        Task AddDiscountsForSale(IEnumerable<SaleDiscount> discounts, CancellationToken cancellationToken);
        Task<Sale?> GetExistingSaleForUser(Guid requestUserId, CancellationToken cancellationToken);
        Task RemoveAllDiscountsForSale(Guid saleId, CancellationToken cancellationToken);
        Task RemoveAllItemsForSale(Guid saleId, CancellationToken cancellationToken);
        Task<Sale?> GetCompleteSale(Guid saleId, CancellationToken cancellationToken);
        Task CommitAsync(CancellationToken cancellationToken);
    }
}