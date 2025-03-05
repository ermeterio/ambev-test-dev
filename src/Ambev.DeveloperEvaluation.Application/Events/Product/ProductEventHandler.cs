using Ambev.DeveloperEvaluation.Domain.Entities.Product;
using Ambev.DeveloperEvaluation.Domain.Events.Product;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Rebus.Handlers;

namespace Ambev.DeveloperEvaluation.Application.Events.Product
{
    public class ProductEventHandler : IHandleMessages<CreateProductEvent>, IHandleMessages<UpdateProductEvent>
    {
        private readonly IProductHistoryRepository _productHistoryRepository;

        public ProductEventHandler(IProductHistoryRepository productHistoryRepository)
        {
            _productHistoryRepository = productHistoryRepository;
        }

        public Task Handle(CreateProductEvent message)
        {
            return _productHistoryRepository.AddAsync(new ProductHistory
            {
                Id = Guid.NewGuid(),
                ProductId = message.Id.ToString(),
                Price = message.Price,
                StockInitial = message.Stock,
                StockFinal = message.Stock,
                Date = message.CreatedAt,
            });
        }

        public Task Handle(UpdateProductEvent message)
        {
            return _productHistoryRepository.AddAsync(new ProductHistory
            {
                Id = Guid.NewGuid(),
                ProductId = message.Id.ToString(),
                Price = message.Price,
                StockInitial = message.ActualStock,
                StockFinal = message.NewStock,
                Date = message.UpdatedAt
            });
        }
    }
}