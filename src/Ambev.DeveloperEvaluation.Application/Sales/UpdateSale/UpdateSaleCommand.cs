using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Sales.UpdateSale
{
    public class UpdateSaleCommand : BaseSale, IRequest<UpdateSaleResult>
    {
        public Guid Id { get; set; }
    }
}