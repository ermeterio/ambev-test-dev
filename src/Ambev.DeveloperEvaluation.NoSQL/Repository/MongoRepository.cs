using Ambev.DeveloperEvaluation.Domain.Common;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using MongoDB.Driver;

namespace Ambev.DeveloperEvaluation.MongoDB.Repository
{
    public class MongoRepository<T> : IRepository<T> where T : BaseEntity
    {
        protected readonly IMongoCollection<T> Collection;

        public MongoRepository(IMongoClient mongoClient, MongoDbSettings settings)
        {
            var database = mongoClient.GetDatabase(settings.DatabaseName);
            Collection = database.GetCollection<T>(typeof(T).Name.ToLower());
        }

        public async Task<IEnumerable<T>> GetAllAsync() =>
            await Collection.Find(_ => true).ToListAsync();

        public async Task<T?> GetByIdAsync(Guid id) =>
            await Collection.Find(e => e.Id == id).FirstOrDefaultAsync();

        public async Task AddAsync(T entity) =>
            await Collection.InsertOneAsync(entity);

        public async Task UpdateAsync(Guid id, T entity) =>
            await Collection.ReplaceOneAsync(e => e.Id == id, entity);

        public async Task DeleteAsync(T entity) =>
            await Collection.DeleteOneAsync(e => e.Id == entity.Id);
    }
}