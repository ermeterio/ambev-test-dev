namespace Ambev.DeveloperEvaluation.WebApi.Features.Companies.CreateCompany;

public class CreateCompanyRequest
{
    public required string Name { get; set; }
    public Guid? ParentCompanyId { get; set; }
    public required string Cnpj { get; set; }
}