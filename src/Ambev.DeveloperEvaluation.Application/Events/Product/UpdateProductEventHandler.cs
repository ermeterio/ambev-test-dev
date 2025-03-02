using Ambev.DeveloperEvaluation.Domain.Entities.Product;
using Ambev.DeveloperEvaluation.Domain.Events.Product;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Rebus.Handlers;

namespace Ambev.DeveloperEvaluation.Application.Events.Product
{
    public class UpdateProductEventHandler : IHandleMessages<UpdateProductEvent>
    {
        private readonly IProductHistoryRepository _productHistoryRepository;

        public UpdateProductEventHandler(IProductHistoryRepository productHistoryRepository)
        {
            _productHistoryRepository = productHistoryRepository;
        }

        public Task Handle(UpdateProductEvent message)
        {
            return _productHistoryRepository.AddAsync(new ProductHistory
            {
                Id = Guid.NewGuid(),
                ProductId = message.Id,
                Price = message.Price,
                StockInitial = message.ActualStock,
                StockFinal = message.NewStock,
                Date = message.UpdatedAt
            });
        }
    }
}