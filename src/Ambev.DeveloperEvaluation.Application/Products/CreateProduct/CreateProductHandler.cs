using Ambev.DeveloperEvaluation.Domain.Entities.Product;
using Ambev.DeveloperEvaluation.Domain.Events.Product;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using AutoMapper;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Logging;
using Rebus.Bus;

namespace Ambev.DeveloperEvaluation.Application.Products.CreateProduct
{
    public class CreateProductHandler : IRequestHandler<CreateProductCommand, CreateProductResult>
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IProductRepository _productRepository;
        private readonly ICompanyRepository _companyRepository;
        private readonly ILogger<CreateProductHandler> _logger;
        private readonly IMapper _mapper;
        private readonly IBus _bus;

        public CreateProductHandler(IProductRepository productRepository, IMapper mapper, ICompanyRepository companyRepository, ICategoryRepository categoryRepository, IBus bus, ILogger<CreateProductHandler> logger)
        {
            _productRepository = productRepository;
            _mapper = mapper;
            _companyRepository = companyRepository;
            _categoryRepository = categoryRepository;
            _bus = bus;
            _logger = logger;
        }

        public async Task<CreateProductResult> Handle(CreateProductCommand request, CancellationToken cancellationToken)
        {
            var validator = new CreateProductValidator();
            var validationResult = await validator.ValidateAsync(request, cancellationToken);

            if (!validationResult.IsValid)
                throw new ValidationException(validationResult.Errors);
            
            _ = await _companyRepository.GetByIdAsync(request.CompanyId) ??
                                throw new InvalidOperationException($"Company with Id {request.CompanyId} does not exist");

            _ = await _categoryRepository.GetByIdAsync(request.CategoryId) ??
                throw new InvalidOperationException($"Category with Id {request.CategoryId} does not exist");

            var existingProduct = await _productRepository.GetByNameAndCategoryAsync(request.Name, request.CategoryId, cancellationToken);
            if (existingProduct is not null)
                throw new InvalidOperationException($"Product with Name {request.Name} already exists");

            var product = _mapper.Map<Product>(request);

            _logger.LogInformation("Creating product {@Product}", product);

            var createdProduct = await _productRepository.AddAsync(product, cancellationToken) ??
                                 throw new InvalidOperationException("Error creating Product");

            var @event = _mapper.Map<CreateProductEvent>(createdProduct);
            await _bus.Send(@event);

            return _mapper.Map<CreateProductResult>(createdProduct);
        }
    }
}