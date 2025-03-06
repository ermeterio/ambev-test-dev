using Ambev.DeveloperEvaluation.Domain.Entities.Product;

namespace Ambev.DeveloperEvaluation.Domain.Repositories
{
    public interface IDiscountRepository : IRepository<Discount>
    {
        public Task<Discount?> GetIdenticalDiscount(Discount entity, CancellationToken cancellationToken);
    }
}