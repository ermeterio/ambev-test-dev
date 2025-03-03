using Ambev.DeveloperEvaluation.Domain.Entities.Product;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using AutoMapper;
using FluentValidation;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Products.CreateProduct
{
    public class CreateProductHandler : IRequestHandler<CreateProductCommand, CreateProductResult>
    {
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;

        public CreateProductHandler(IProductRepository productRepository, IMapper mapper)
        {
            _productRepository = productRepository;
            _mapper = mapper;
        }

        public async Task<CreateProductResult> Handle(CreateProductCommand request, CancellationToken cancellationToken)
        {
            var validator = new CreateProductValidator();
            var validationResult = await validator.ValidateAsync(request, cancellationToken);

            if (!validationResult.IsValid)
                throw new ValidationException(validationResult.Errors);

            var existingProduct = await _productRepository.GetByNameAndCategoryAsync(request.Name, request.CategoryId, cancellationToken);
            if (existingProduct is not null)
                throw new InvalidOperationException($"Product with Name {request.Name} already exists");

            var product = _mapper.Map<Product>(request);
            var createdProduct = await _productRepository.AddAsync(product, cancellationToken);
            if (createdProduct is null)
                throw new InvalidOperationException("Error creating Product");

            return _mapper.Map<CreateProductResult>(createdProduct);
        }
    }
}