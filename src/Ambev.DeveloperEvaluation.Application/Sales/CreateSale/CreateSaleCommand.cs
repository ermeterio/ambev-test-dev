using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Sales.CreateSale
{
    public class CreateSaleCommand : BaseSale, IRequest<CreateSaleResult>
    { }
}