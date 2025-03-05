using Ambev.DeveloperEvaluation.Domain.Entities.Company;

namespace Ambev.DeveloperEvaluation.Application.Companies.GetCompany
{
    public class GetCompanyResult
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public bool IsBranch => ParentCompanyId is not null;
        public Guid? ParentCompanyId { get; set; }
        public string Cnpj { get; set; } = string.Empty;
    }
}