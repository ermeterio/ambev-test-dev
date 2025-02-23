using Ambev.DeveloperEvaluation.Domain.Common;

namespace Ambev.DeveloperEvaluation.Domain.Entities.Company
{
    public class Company : BaseEntity
    {
        public required string Name { get; set; }
        public bool IsBranch { get; set; }
        public Guid? ParentCompanyId { get; set; }
        public required string Cnpj { get; set; }
        public Company? ParentCompany { get; set; }
    }
}