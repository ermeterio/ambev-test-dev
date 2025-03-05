using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Products.DeleteDiscount
{
    public class DeleteDiscountCommand : IRequest<DeleteDiscountResult>
    {
        public Guid Id { get; set; }

        public DeleteDiscountCommand(Guid id)
        {
            Id = id;
        }
    }
}