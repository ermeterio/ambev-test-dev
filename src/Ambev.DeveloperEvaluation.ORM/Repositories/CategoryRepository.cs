using Ambev.DeveloperEvaluation.Domain.Entities.Product;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Ambev.DeveloperEvaluation.ORM.Repositories
{
    public class CategoryRepository : BaseRepository<Category>, ICategoryRepository
    {
        public CategoryRepository(DefaultContext context) : base(context)
        {
        }

        public Task<Category?> GetByNameAsync(string name, Guid companyId)
        {
            return DbSet.FirstOrDefaultAsync(c => c.Name == name && c.CompanyId == companyId);
        }
    }
}