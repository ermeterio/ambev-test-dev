using Ambev.DeveloperEvaluation.Domain.Entities.Product;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using AutoMapper;
using FluentValidation;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Products.CreateDiscount
{
    public class CreateDiscountHandler : IRequestHandler<CreateDiscountCommand, CreateDiscountResult>
    {
        private readonly IProductRepository _productRepository;
        private readonly IDiscountRepository _discountRepository;
        private readonly IMapper _mapper;

        public CreateDiscountHandler(IProductRepository productRepository, IDiscountRepository discountRepository, IMapper mapper)
        {
            _productRepository = productRepository;
            _discountRepository = discountRepository;
            _mapper = mapper;
        }

        public async Task<CreateDiscountResult> Handle(CreateDiscountCommand request, CancellationToken cancellationToken)
        {
            var validator = new CreateDiscountValidator();
            var validationResult = await validator.ValidateAsync(request, cancellationToken);

            if (!validationResult.IsValid)
                throw new ValidationException(validationResult.Errors);

            var existingProduct = await _productRepository.GetByIdAsync(request.ProductId);
            if (existingProduct is null)
                throw new InvalidOperationException($"Product with Id {request.ProductId} not exists");

            var discount = _mapper.Map<Discount>(request);

            await _discountRepository.AddAsync(discount, cancellationToken);

            return _mapper.Map<CreateDiscountResult>(discount);
        }
    }
}