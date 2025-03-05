using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Products.UpdateProduct
{
    public class UpdateProductCommand : BaseProduct, IRequest<UpdateProductResult>
    {
        public Guid Id { get; set; }
    }
}