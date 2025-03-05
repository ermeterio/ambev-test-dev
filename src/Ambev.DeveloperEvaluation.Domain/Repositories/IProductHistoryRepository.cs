using Ambev.DeveloperEvaluation.Domain.Entities.Product;

namespace Ambev.DeveloperEvaluation.Domain.Repositories
{
    public interface IProductHistoryRepository : IRepository<ProductHistory>
    {
        Task<IEnumerable<ProductHistory>> GetByProductIdAsync(Guid productId);
    }
}