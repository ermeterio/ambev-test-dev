using Ambev.DeveloperEvaluation.Domain.Common;

namespace Ambev.DeveloperEvaluation.Domain.Entities.Company
{
    public class Company : BaseEntity
    {
        public string Name { get; set; } = string.Empty;
        public bool IsBranch => ParentCompanyId is not null;
        public Guid? ParentCompanyId { get; set; }
        public string Cnpj { get; set; } = string.Empty;
        public Company? ParentCompany { get; set; }

        public void Update(Company company)
        {
            if(Name != company.Name)
                Name = company.Name;
            if (Cnpj != company.Cnpj)
                Cnpj = company.Cnpj;
            if (ParentCompanyId != company.ParentCompanyId)
                ParentCompanyId = company.ParentCompanyId;
        }

        public Company()
        { }

        public Company(string name, Guid? parentCompanyId, string cnpj)
        {
            Name = name;
            ParentCompanyId = parentCompanyId;
            Cnpj = cnpj;
        }
    }
}