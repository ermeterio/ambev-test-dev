﻿using Ambev.DeveloperEvaluation.Domain.Entities.Sale;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using AutoMapper;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Ambev.DeveloperEvaluation.Application.Sales.UpdateSale
{
    public class UpdateSaleHandler : IRequestHandler<UpdateSaleCommand, UpdateSaleResult>
    {
        private readonly IProductRepository _productRepository;
        private readonly ICompanyRepository _companyRepository;
        private readonly ILogger<UpdateSaleHandler> _logger;
        private readonly ISaleRepository _saleRepository;
        private readonly IMapper _mapper;
        public UpdateSaleHandler(ISaleRepository saleRepository, IMapper mapper, IProductRepository productRepository, ICompanyRepository companyRepository, ILogger<UpdateSaleHandler> logger)
        {
            _productRepository = productRepository;
            _companyRepository = companyRepository;
            _logger = logger;
            _saleRepository = saleRepository;
            _mapper = mapper;
        }

        public async Task<UpdateSaleResult> Handle(UpdateSaleCommand request, CancellationToken cancellationToken)
        {
            var validator = new UpdateSaleValidator();
            var validationResult = await validator.ValidateAsync(request, cancellationToken);

            if (!validationResult.IsValid)
                throw new ValidationException(validationResult.Errors);

            var sale = await _saleRepository.GetByIdAsync(request.Id) ??
                       throw new InvalidOperationException($"Sale with Id {request.Id} not found");

            _ = await _companyRepository.GetByIdAsync(request.CompanyId) ??
                                throw new InvalidOperationException($"Company with Id {request.CompanyId} does not exist");

            var saleMapper = _mapper.Map<Sale>(request);
            sale.Update(saleMapper);

            _logger.LogInformation("Updating sale {@Sale}", sale);

            await _saleRepository.RemoveAllItemsForSale(sale.Id, cancellationToken);

            var updatedSale = await _saleRepository.UpdateAsync(sale, cancellationToken);
            if (updatedSale is null)
                return new() { Success = false };

            var discounts = updatedSale.Discounts?.ToList() ?? [];
            foreach (var item in sale.Items!)
            {
                var product = await _productRepository.GetByIdAsync(item.ProductId) ??
                              throw new InvalidOperationException($"Product with Id {item.ProductId} not found");

                item.Product = product;

                if (product.MaxItemsForSale < item.Quantity)
                {
                    throw new InvalidOperationException($"Product with Id {item.ProductId} has a limit of {product.MaxItemsForSale} items per sale");
                }

                var discount = await _productRepository.GetDiscountForProductAsync(item.ProductId, item.Quantity, cancellationToken);
                if (discount is not null)
                {
                    _logger.LogInformation("Updating discount {@discount}", discount);
                    discounts.Add(new()
                    {
                        DiscountId = discount.Id,
                        SaleId = updatedSale.Id
                    });
                }
            }

            if (discounts.Count > 0)
            {
                await _saleRepository.RemoveAllDiscountsForSale(updatedSale.Id, cancellationToken);
                await _saleRepository.AddDiscountsForSale(discounts, cancellationToken);
            }

            await _saleRepository.CommitAsync(cancellationToken);

            return _mapper.Map<UpdateSaleResult>(updatedSale);
        }
    }
}