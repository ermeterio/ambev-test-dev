namespace Ambev.DeveloperEvaluation.Application.Companies
{
    public class BaseCompany
    {
        public required string Name { get; set; }
        public Guid? ParentCompanyId { get; set; }
        public required string Cnpj { get; set; }
    }
}