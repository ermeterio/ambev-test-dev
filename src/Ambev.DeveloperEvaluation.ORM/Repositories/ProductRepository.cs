using Ambev.DeveloperEvaluation.Domain.Entities.Product;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Ambev.DeveloperEvaluation.ORM.Repositories
{
    public class ProductRepository : BaseRepository<Product>, IProductRepository
    {
        public ProductRepository(DefaultContext context) : base(context)
        { }

        public Task<Product?> GetByNameAndCategoryAsync(string name, Guid categoryId, CancellationToken cancellationToken)
            => DbSet.FirstOrDefaultAsync(p => p.Name == name && p.CategoryId == categoryId,
                    cancellationToken);

        public Task<Discount?> GetDiscountForProductAsync(Guid productId, int quantity,
            CancellationToken cancellationToken)
            => Context.ProductDiscounts!.FirstOrDefaultAsync(d =>
                d.ProductId == productId && d.Quantity >= quantity && d.StartDate >= DateTime.Today &&
                d.EndDate <= DateTime.Today, cancellationToken);

    }
}