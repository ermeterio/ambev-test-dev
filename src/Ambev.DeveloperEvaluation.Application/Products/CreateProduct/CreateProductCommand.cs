using Ambev.DeveloperEvaluation.Domain.Entities.Product;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Products.CreateProduct
{
    public class CreateProductCommand : BaseProduct, IRequest<CreateProductResult>
    { }
}