using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Categories.CreateCategory
{
    public class CreateCategoryCommand : IRequest<CreateCategoryResult>
    {
        public string Name { get; set; } = string.Empty;
        public Guid CompanyId { get; set; }
    }
}