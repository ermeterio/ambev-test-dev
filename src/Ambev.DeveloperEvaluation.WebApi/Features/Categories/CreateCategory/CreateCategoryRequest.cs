namespace Ambev.DeveloperEvaluation.WebApi.Features.Categories.CreateCategory;

public class CreateCategoryRequest
{
    public string Name { get; set; } = string.Empty;
    public Guid CompanyId { get; set; }
}
