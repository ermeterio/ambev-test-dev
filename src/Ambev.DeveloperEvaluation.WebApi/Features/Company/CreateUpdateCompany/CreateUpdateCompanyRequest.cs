namespace Ambev.DeveloperEvaluation.WebApi.Features.Company.CreateUpdateCompany;

public class CreateUpdateCompanyRequest
{
    public required string Name { get; set; }
    public Guid? ParentCompanyId { get; set; }
    public required string Cnpj { get; set; }
}