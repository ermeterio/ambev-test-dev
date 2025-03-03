using Ambev.DeveloperEvaluation.Domain.Entities.Sale;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using AutoMapper;
using FluentValidation;
using MediatR;
using Rebus.Bus;

namespace Ambev.DeveloperEvaluation.Application.Sales.UpdateSale
{
    public class UpdateSaleHandler : IRequestHandler<UpdateSaleCommand, UpdateSaleResult>
    {
        private readonly IProductRepository _productRepository;
        private readonly ISaleRepository _saleRepository;
        private readonly IMapper _mapper;
        private readonly IBus _bus;
        public UpdateSaleHandler(ISaleRepository saleRepository, IMapper mapper, IProductRepository productRepository, IBus bus)
        {
            _saleRepository = saleRepository;
            _mapper = mapper;
            _productRepository = productRepository;
            _bus = bus;
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

            var saleMapper = _mapper.Map<Sale>(request);
            sale.Update(saleMapper);

            var updatedSale = await _saleRepository.UpdateAsync(sale, cancellationToken);
            if(updatedSale is null)
                return new() { Success = false };

            var discounts = updatedSale?.Discounts?.ToList() ?? [];
            foreach (var item in sale.Items!)
            {
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