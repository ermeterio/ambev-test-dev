namespace Ambev.DeveloperEvaluation.WebApi.Features.Categories.GetCategory;

public class GetCategoryResponse
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public Guid CompanyId { get; set; }
}