namespace Ambev.DeveloperEvaluation.WebApi.Features.Companies.UpdateCompany;

public class UpdateCompanyResponse
{
    public Guid Id { get; set; }
    public required string Name { get; set; }
    public Guid? ParentCompanyId { get; set; }
    public required string Cnpj { get; set; }
    public bool IsBranch { get; set; }
}