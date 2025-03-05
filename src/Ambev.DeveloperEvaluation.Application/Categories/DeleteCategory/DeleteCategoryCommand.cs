using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Categories.DeleteCategory
{
    public class DeleteCategoryCommand : IRequest<DeleteCategoryResult>
    {
        public DeleteCategoryCommand(Guid id)
        {
            Id = id;
        }
        public Guid Id { get; set; }
    }
}