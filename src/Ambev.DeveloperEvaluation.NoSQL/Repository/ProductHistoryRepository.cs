using Ambev.DeveloperEvaluation.Domain.Entities.Product;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using MongoDB.Driver;

namespace Ambev.DeveloperEvaluation.MongoDB.Repository
{
    public class ProductHistoryRepository(IMongoClient mongoClient, MongoDbSettings settings)
        : MongoRepository<ProductHistory>(mongoClient, settings), IProductHistoryRepository
    {
        public async Task<IEnumerable<ProductHistory>> GetByProductIdAsync(Guid productId) =>
            await Collection.Find(e => e.ProductId == productId).ToListAsync();
    }
}