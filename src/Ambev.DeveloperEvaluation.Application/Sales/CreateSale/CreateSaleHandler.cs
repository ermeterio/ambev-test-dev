using Ambev.DeveloperEvaluation.Domain.Entities.Product;
using Ambev.DeveloperEvaluation.Domain.Entities.Sale;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using AutoMapper;
using FluentValidation;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Sales.CreateSale
{
    public class CreateSaleHandler : IRequestHandler<CreateSaleCommand, CreateSaleResult>
    {
        private readonly IMapper _mapper;
        private readonly ISaleRepository _saleRepository;
        private readonly IProductRepository _productRepository;

        public CreateSaleHandler(ISaleRepository saleRepository, IMapper mapper, IProductRepository productRepository)
        {
            _mapper = mapper;
            _saleRepository = saleRepository;
            _productRepository = productRepository;
        }

        public async Task<CreateSaleResult> Handle(CreateSaleCommand request, CancellationToken cancellationToken)
        {
            var validator = new CreateSaleValidator();
            var validationResult = await validator.ValidateAsync(request, cancellationToken);

            if (!validationResult.IsValid)
                throw new ValidationException(validationResult.Errors);

            var existingSale = await _saleRepository.GetExistingSaleForUser(request.UserId, cancellationToken);
            if (existingSale is not null)
                throw new InvalidOperationException("There is already a sale for the user");

            var sale = _mapper.Map<Sale>(request);
            var createdSale = await _saleRepository.AddAsync(sale, cancellationToken);
            if (createdSale is null)
                throw new InvalidOperationException("Error creating Sale");

            var discounts = createdSale.Discounts?.ToList() ?? [];
            foreach (var item in sale.Items!)
            {
                var discount = await _productRepository.GetDiscountForProductAsync(item.ProductId, item.Quantity, cancellationToken);
                if (discount is not null)
                {
                    discounts.Add(new ()
                    {
                        DiscountId = discount.Id,
                        SaleId = createdSale.Id
                    });
                }
            }
            if (discounts.Any())
            {
                await _saleRepository.RemoveAllDiscountsForSale(createdSale.Id, cancellationToken);
                await _saleRepository.AddDiscountsForSale(discounts, cancellationToken);
            }

            return _mapper.Map<CreateSaleResult>(createdSale);
        }
    }
}