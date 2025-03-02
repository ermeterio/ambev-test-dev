using Ambev.DeveloperEvaluation.Domain.Entities.Product;
using Ambev.DeveloperEvaluation.Domain.Events.Product;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Rebus.Handlers;

namespace Ambev.DeveloperEvaluation.Application.Events.Product
{
    public class CreateProductEventHandler : IHandleMessages<CreateProductEvent>
    {
        private readonly IProductHistoryRepository _productHistoryRepository;

        public CreateProductEventHandler(IProductHistoryRepository productHistoryRepository)
        {
            _productHistoryRepository = productHistoryRepository;
        }

        public Task Handle(CreateProductEvent message)
        {
            return _productHistoryRepository.AddAsync(new ProductHistory
            {
                Id = Guid.NewGuid(),
                ProductId = message.Id,
                Price = message.Price,
                StockInitial = message.Stock,
                StockFinal = message.Stock,
                Date = message.CreatedAt
            });
        }
    }
}