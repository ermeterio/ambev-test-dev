using Ambev.DeveloperEvaluation.Domain.Entities.Product;
using Ambev.DeveloperEvaluation.Domain.Entities.Sale;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using AutoMapper;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Ambev.DeveloperEvaluation.Application.Sales.CreateSale
{
    public class CreateSaleHandler : IRequestHandler<CreateSaleCommand, CreateSaleResult>
    {
        private readonly IMapper _mapper;
        private readonly ISaleRepository _saleRepository;
        private readonly ILogger<CreateSaleHandler> _logger;
        private readonly ICompanyRepository _companyRepository;
        private readonly IProductRepository _productRepository;

        public CreateSaleHandler(ISaleRepository saleRepository, IMapper mapper, IProductRepository productRepository, ICompanyRepository companyRepository, ILogger<CreateSaleHandler> logger)
        {
            _mapper = mapper;
            _saleRepository = saleRepository;
            _productRepository = productRepository;
            _companyRepository = companyRepository;
            _logger = logger;
        }

        public async Task<CreateSaleResult> Handle(CreateSaleCommand request, CancellationToken cancellationToken)
        {
            var validator = new CreateSaleValidator();
            var validationResult = await validator.ValidateAsync(request, cancellationToken);

            if (!validationResult.IsValid)
                throw new ValidationException(validationResult.Errors);

            _ = await _companyRepository.GetByIdAsync(request.CompanyId) ?? 
                throw new InvalidOperationException($"Company with Id {request.CompanyId} does not exist");

            var existingSale = await _saleRepository.GetExistingSaleForUser(request.UserId, cancellationToken);
            if(existingSale is not null)
                throw new InvalidOperationException("There is already a sale for the user");

            var sale = _mapper.Map<Sale>(request);

            _logger.LogInformation("Creating sale {@Sale}", sale);

            var createdSale = await _saleRepository.AddAsync(sale, cancellationToken) ??
                throw new InvalidOperationException("Error creating Sale");

            var discounts = createdSale.Discounts?.ToList() ?? [];
            foreach (var item in sale.Items!)
            {
                var product = await _productRepository.GetByIdAsync(item.ProductId) ??
                              throw new InvalidOperationException($"Product with Id {item.ProductId} not found");

                if (product.MaxItemsForSale < item.Quantity)
                {
                    throw new InvalidOperationException($"Product with Id {item.ProductId} has a limit of {product.MaxItemsForSale} items per sale");
                }

                item.Product = product;

                var discount = await _productRepository.GetDiscountForProductAsync(item.ProductId, item.Quantity, cancellationToken);
                if (discount is not null)
                {
                    _logger.LogInformation("Creating discount {@discount}", discount);

                    discounts.Add(new()
                    {
                        DiscountId = discount.Id,
                        SaleId = createdSale.Id
                    });
                }
            }

            if (discounts.Count > 0)
            {
                await _saleRepository.RemoveAllDiscountsForSale(createdSale.Id, cancellationToken);
                await _saleRepository.AddDiscountsForSale(discounts, cancellationToken);
            }

            await _saleRepository.CommitAsync(cancellationToken);

            return _mapper.Map<CreateSaleResult>(createdSale);
        }
    }
}