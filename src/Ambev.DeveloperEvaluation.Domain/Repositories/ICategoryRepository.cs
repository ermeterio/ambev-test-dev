using Ambev.DeveloperEvaluation.Domain.Entities.Product;

namespace Ambev.DeveloperEvaluation.Domain.Repositories
{
    public interface ICategoryRepository : IRepository<Category>
    {
        public Task<Category?> GetByNameAsync(string name, Guid companyId);
    }
}