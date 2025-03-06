using Ambev.DeveloperEvaluation.Domain.Entities.Product;
using Ambev.DeveloperEvaluation.Domain.Events.Product;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using AutoMapper;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Logging;
using Rebus.Bus;

namespace Ambev.DeveloperEvaluation.Application.Products.UpdateProduct
{
    public class UpdateProductHandler : IRequestHandler<UpdateProductCommand, UpdateProductResult>
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IProductRepository _productRepository;
        private readonly ICompanyRepository _companyRepository;
        private readonly ILogger<UpdateProductHandler> _logger;
        private readonly IMapper _mapper;
        private readonly IBus _bus;

        public UpdateProductHandler(IProductRepository productRepository, IMapper mapper, ICompanyRepository companyRepository, ICategoryRepository categoryRepository, IBus bus, ILogger<UpdateProductHandler> logger)
        {
            _productRepository = productRepository;
            _mapper = mapper;
            _companyRepository = companyRepository;
            _categoryRepository = categoryRepository;
            _bus = bus;
            _logger = logger;
        }

        public async Task<UpdateProductResult> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
        {
            var validator = new UpdateProductValidator();
            var validationResult = await validator.ValidateAsync(request, cancellationToken);

            if (!validationResult.IsValid)
                throw new ValidationException(validationResult.Errors);

            var product = await _productRepository.GetByIdAsync(request.Id) ?? 
                throw new InvalidOperationException($"Product with Id {request.Id} not found");

            _ = await _companyRepository.GetByIdAsync(request.CompanyId) ??
                throw new InvalidOperationException($"Company with Id {request.CompanyId} does not exist");

            _ = await _categoryRepository.GetByIdAsync(request.CategoryId) ??
                                 throw new InvalidOperationException($"Category with Id {request.CategoryId} does not exist");

            var productMapper = _mapper.Map<Product>(request);
            product.Update(productMapper);

            _logger.LogInformation("Updating product {@Product}", product);

            var updatedProduct = await _productRepository.UpdateAsync(product, cancellationToken);

            var @event = _mapper.Map<UpdateProductEvent>(updatedProduct);
            await _bus.Publish(@event);

            return updatedProduct is null ? new() { Success = false } : _mapper.Map<UpdateProductResult>(updatedProduct);
        }
    }
}