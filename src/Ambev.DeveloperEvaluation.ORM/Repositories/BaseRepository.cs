using Ambev.DeveloperEvaluation.Domain.Common;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Ambev.DeveloperEvaluation.ORM.Repositories
{
    public class BaseRepository<T> : IRepository<T> where T : BaseEntity
    {
        protected readonly DefaultContext Context;
        protected DbSet<T> DbSet => Context.Set<T>();

        public BaseRepository(DefaultContext context)
        {
            Context = context;
        }

        public async Task<IEnumerable<T>> GetAllAsync()
            => await DbSet.ToListAsync();

        public Task<T?> GetByIdAsync(Guid id)
            => DbSet.FirstOrDefaultAsync(s => s.Id == id);

        public async Task<T?> AddAsync(T entity, CancellationToken cancellationToken = default)
        {
            await DbSet.AddAsync(entity, cancellationToken);
            await Context.SaveChangesAsync(cancellationToken);
            return entity;
        }

        public async Task<T?> UpdateAsync(T entity, CancellationToken cancellationToken = default)
        {
            DbSet.Update(entity);
            await Context.SaveChangesAsync(cancellationToken);
            return entity;
        }

        public async Task<bool> DeleteAsync(T entity, CancellationToken cancellationToken = default)
        {
            entity.IsDeleted = true;
            DbSet.Update(entity);
            await Context.SaveChangesAsync(cancellationToken);
            return true;
        }

        public Task AddRange(IList<T> entities, CancellationToken cancellationToken = default)
        {
            DbSet.AddRangeAsync(entities, cancellationToken);
            return Context.SaveChangesAsync(cancellationToken);
        }
    }
}
