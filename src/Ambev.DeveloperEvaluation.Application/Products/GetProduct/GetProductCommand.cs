using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Products.GetProduct
{
    public class GetProductCommand : IRequest<GetProductResult>
    {
        public GetProductCommand(Guid id)
        {
            Id = id;
        }
        public Guid Id { get; set; }
    }
}