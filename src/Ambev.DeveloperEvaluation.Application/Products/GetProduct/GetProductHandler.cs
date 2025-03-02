using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Products.GetProduct
{
    public class GetProductHandler : IRequestHandler<GetProductCommand, GetProductResult>
    {
        public Task<GetProductResult> Handle(GetProductCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}