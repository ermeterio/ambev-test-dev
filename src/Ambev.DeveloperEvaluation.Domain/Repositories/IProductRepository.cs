using Ambev.DeveloperEvaluation.Domain.Entities.Product;

namespace Ambev.DeveloperEvaluation.Domain.Repositories
{
    public interface IProductRepository : IRepository<Product>
    {
        Task<Product?> GetByNameAndCategoryAsync(string name, Guid categoryId, CancellationToken cancellationToken);
        Task<Discount?> GetDiscountForProductAsync(Guid productId, int quantity,
            CancellationToken cancellationToken);
        Task<bool> ExistsProductCategoryAsync(Guid categoryId, CancellationToken cancellationToken);
    }
}