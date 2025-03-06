using Ambev.DeveloperEvaluation.Domain.Common;
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
                d.ProductId == productId && d.Quantity >= quantity && d.StartDate.ToUniversalTime() <= DateTime.UtcNow &&
                d.EndDate.ToUniversalTime() >= DateTime.UtcNow, cancellationToken);

        public Task<bool> ExistsProductCategoryAsync(Guid categoryId, CancellationToken cancellationToken)
            => DbSet!.AnyAsync(c => c.CategoryId == categoryId, cancellationToken);

        public async Task<(IEnumerable<Product>, PaginatedFilter)> GetProductsByFilterAsync(Product productFilter, PaginatedFilter paginatedFilter, CancellationToken cancellationToken)
        {
            paginatedFilter.TotalItems = await DbSet.CountAsync(cancellationToken);
            paginatedFilter.TotalPages = Convert.ToInt32(paginatedFilter.TotalItems / paginatedFilter.PageSize);
            
            if (paginatedFilter.TotalPages == 0)
                paginatedFilter.TotalPages = 1;

            var data = DbSet.AsQueryable();

            if(!string.IsNullOrEmpty(productFilter.Name))
                data = data.Where(p => p.Name.Contains(productFilter.Name));

            if (productFilter.CategoryId != default)
                data = data.Where(p => p.CategoryId == productFilter.CategoryId);

            return (await data.Skip((paginatedFilter.CurrentPage - 1) * paginatedFilter.PageSize)
                .Take(paginatedFilter.PageSize)
                .ToListAsync(cancellationToken), paginatedFilter);
        }
    }
}