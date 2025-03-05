using Ambev.DeveloperEvaluation.Domain.Entities.Sale;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using AutoMapper;
using FluentValidation;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Sales.UpdateSale
{
    public class UpdateSaleHandler : IRequestHandler<UpdateSaleCommand, UpdateSaleResult>
    {
        private readonly IProductRepository _productRepository;
        private readonly ICompanyRepository _companyRepository;
        private readonly ISaleRepository _saleRepository;
        private readonly IMapper _mapper;
        public UpdateSaleHandler(ISaleRepository saleRepository, IMapper mapper, IProductRepository productRepository, ICompanyRepository companyRepository)
        {
            _productRepository = productRepository;
            _companyRepository = companyRepository;
            _saleRepository = saleRepository;
            _mapper = mapper;
        }

        public async Task<UpdateSaleResult> Handle(UpdateSaleCommand request, CancellationToken cancellationToken)
        {
            var validator = new UpdateSaleValidator();
            var validationResult = await validator.ValidateAsync(request, cancellationToken);

            if (!validationResult.IsValid)
                throw new ValidationException(validationResult.Errors);

            var sale = await _saleRepository.GetByIdAsync(request.Id);
            if (sale is null)
                throw new InvalidOperationException($"Sale with Id {request.Id} not found");

            var existsCompany = await _companyRepository.GetByIdAsync(request.CompanyId);
            if (existsCompany is null)
                throw new InvalidOperationException($"Company with Id {request.CompanyId} does not exist");

            var saleMapper = _mapper.Map<Sale>(request);
            sale.Update(saleMapper);

            await _saleRepository.RemoveAllItemsForSale(sale.Id, cancellationToken);

            var updatedSale = await _saleRepository.UpdateAsync(sale, cancellationToken);
            if(updatedSale is null)
                return new() { Success = false };

            var discounts = updatedSale?.Discounts?.ToList() ?? [];
            foreach (var item in sale.Items!)
            {
                var product = await _productRepository.GetByIdAsync(item.ProductId);
                if (product is null)
                    throw new InvalidOperationException($"Product with Id {item.ProductId} not found");

                item.Product = product;

                if (product.MaxItemsForSale < item.Quantity)
                {
                    throw new InvalidOperationException($"Product with Id {item.ProductId} has a limit of {product.MaxItemsForSale} items per sale");
                }

                var discount = await _productRepository.GetDiscountForProductAsync(item.ProductId, item.Quantity, cancellationToken);
                if (discount is not null)
                {
                    discounts.Add(new ()
                    {
                        DiscountId = discount.Id,
                        SaleId = updatedSale!.Id
                    });
                }
            }

            if (discounts.Any())
            {
                await _saleRepository.RemoveAllDiscountsForSale(updatedSale!.Id, cancellationToken);
                await _saleRepository.AddDiscountsForSale(discounts, cancellationToken);
            }

            return  _mapper.Map<UpdateSaleResult>(updatedSale);
        }
    }
}