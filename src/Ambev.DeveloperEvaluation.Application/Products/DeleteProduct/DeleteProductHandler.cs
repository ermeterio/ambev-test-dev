using Ambev.DeveloperEvaluation.Domain.Repositories;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Ambev.DeveloperEvaluation.Application.Products.DeleteProduct
{
    public class DeleteProductHandler : IRequestHandler<DeleteProductCommand, DeleteProductResult>
    {
        private readonly IProductRepository _productRepository;
        private readonly ILogger<DeleteProductHandler> _logger;
        public DeleteProductHandler(IProductRepository productRepository, ILogger<DeleteProductHandler> logger)
        {
            _productRepository = productRepository;
            _logger = logger;
        }

        public async Task<DeleteProductResult> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
        {
            var validator = new DeleteProductValidator();
            var validationResult = await validator.ValidateAsync(request, cancellationToken);

            if (!validationResult.IsValid)
                throw new ValidationException(validationResult.Errors);

            var product = await _productRepository.GetByIdAsync(request.Id);
            if (product == null)
                throw new KeyNotFoundException($"Product with ID {request.Id} not found");

            var success = await _productRepository.DeleteAsync(product, cancellationToken);
            if (!success)
                throw new KeyNotFoundException($"Update for Id {request.Id} not successful");

            _logger.LogInformation("Product {@Product} deleted", product);

            return new DeleteProductResult { Success = true };
        }
    }
}