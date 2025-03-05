namespace Ambev.DeveloperEvaluation.WebApi.Features.Companies.GetCompany;

public class GetCompanyResponse
{
    public Guid Id { get; set; }
    public required string Name { get; set; }
    public bool IsBranch => ParentCompanyId is not null;
    public Guid? ParentCompanyId { get; set; }
    public required string Cnpj { get; set; }
    public Domain.Entities.Company.Company? ParentCompany { get; set; }
}