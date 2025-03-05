namespace Ambev.DeveloperEvaluation.WebApi.Features.Companies;

public class BaseCompany
{
    public string Name { get; set; } = string.Empty;
    public Guid? ParentCompanyId { get; set; }
    public string Cnpj { get; set; } = string.Empty;
}
