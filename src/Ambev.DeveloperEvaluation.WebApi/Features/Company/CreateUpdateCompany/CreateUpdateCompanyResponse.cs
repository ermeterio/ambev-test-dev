namespace Ambev.DeveloperEvaluation.WebApi.Features.Company.CreateUpdateCompany;

public class CreateUpdateCompanyResponse
{
    public Guid Id { get; set; }
    public required string Name { get; set; }
    public Guid? ParentCompanyId { get; set; }
    public required string Cnpj { get; set; }
    public bool IsBranch { get; set; }
}