using Ambev.DeveloperEvaluation.Domain.Entities.Company;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Ambev.DeveloperEvaluation.ORM.Repositories
{
    public class CompanyRepository : BaseRepository<Company>, ICompanyRepository
    {
        public CompanyRepository(DefaultContext context) : base(context)
        { }

        public Task<Company?> GetByCnpjAsync(string cnpj, CancellationToken cancellationToken)
            => DbSet.FirstOrDefaultAsync(s => s.Cnpj == cnpj, cancellationToken);
    }
}