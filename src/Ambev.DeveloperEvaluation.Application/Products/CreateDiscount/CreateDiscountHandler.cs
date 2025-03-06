using Ambev.DeveloperEvaluation.Domain.Entities.Product;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using AutoMapper;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Ambev.DeveloperEvaluation.Application.Products.CreateDiscount
{
    public class CreateDiscountHandler : IRequestHandler<CreateDiscountCommand, CreateDiscountResult>
    {
        private readonly IProductRepository _productRepository;
        private readonly IDiscountRepository _discountRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<CreateDiscountHandler> _logger;

        public CreateDiscountHandler(IProductRepository productRepository, IDiscountRepository discountRepository, IMapper mapper, ILogger<CreateDiscountHandler> logger)
        {
            _productRepository = productRepository;
            _discountRepository = discountRepository;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<CreateDiscountResult> Handle(CreateDiscountCommand request, CancellationToken cancellationToken)
        {
            var validator = new CreateDiscountValidator();
            var validationResult = await validator.ValidateAsync(request, cancellationToken);

            if (!validationResult.IsValid)
                throw new ValidationException(validationResult.Errors);

            _ = await _productRepository.GetByIdAsync(request.ProductId) ??
                                  throw new InvalidOperationException($"Product with Id {request.ProductId} not exists");

            var discount = _mapper.Map<Discount>(request);

            var existsDiscount = await _discountRepository.GetIdenticalDiscount(discount, cancellationToken);
            if (existsDiscount is not null)
                throw new InvalidOperationException("Discount already exists");

            _logger.LogInformation("Creating discount {@Discount}", discount);

            await _discountRepository.AddAsync(discount, cancellationToken);

            return _mapper.Map<CreateDiscountResult>(discount);
        }
    }
}