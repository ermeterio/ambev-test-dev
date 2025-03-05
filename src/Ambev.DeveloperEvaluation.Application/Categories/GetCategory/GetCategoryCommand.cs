using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Categories.GetCategory
{
    public class GetCategoryCommand : IRequest<GetCategoryResult>
    {
        public GetCategoryCommand(Guid id)
        {
            Id = id;
        }
        public Guid Id { get; set; }
    }
}
