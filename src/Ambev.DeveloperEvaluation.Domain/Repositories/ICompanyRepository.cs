using Ambev.DeveloperEvaluation.Domain.Entities.Company;

namespace Ambev.DeveloperEvaluation.Domain.Repositories
{
    public interface ICompanyRepository : IRepository<Company>
    {
        public Task<Company?> GetByCnpjAsync(string cnpj, CancellationToken cancellationToken);
    }
}