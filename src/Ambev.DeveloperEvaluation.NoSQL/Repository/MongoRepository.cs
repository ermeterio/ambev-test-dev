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

        public async Task<T?> AddAsync(T entity, CancellationToken cancellationToken)
        {
            await Collection.InsertOneAsync(entity, cancellationToken: cancellationToken);
            return entity;
        }

        public async Task<T?> UpdateAsync(T entity, CancellationToken cancellationToken)
        {
            await Collection.ReplaceOneAsync(e => e.Id == entity.Id, entity, cancellationToken: cancellationToken);
            return entity;
        }

        public async Task<bool> DeleteAsync(T entity, CancellationToken cancellationToken)
        {
            await Collection.DeleteOneAsync(e => e.Id == entity.Id, cancellationToken: cancellationToken);
            return true;
        }

        public Task AddRange(IList<T> entities, CancellationToken cancellationToken = default)
            => Collection.InsertManyAsync(entities, null, cancellationToken);
    }
}