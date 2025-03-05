using Ambev.DeveloperEvaluation.Domain.Repositories;
using FluentValidation;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Products.DeleteDiscount
{
    public class DeleteDiscountHandler : IRequestHandler<DeleteDiscountCommand, DeleteDiscountResult>
    {
        private readonly IDiscountRepository _discountRepository;

        public DeleteDiscountHandler(IDiscountRepository discountRepository)
        {
            _discountRepository = discountRepository;
        }

        public async Task<DeleteDiscountResult> Handle(DeleteDiscountCommand request, CancellationToken cancellationToken)
        {
            var validator = new DeleteDiscountValidator();
            var validationResult = await validator.ValidateAsync(request, cancellationToken);

            if (!validationResult.IsValid)
                throw new ValidationException(validationResult.Errors);

            var discount = await _discountRepository.GetByIdAsync(request.Id);
            if (discount == null)
                throw new KeyNotFoundException($"Discount with ID {request.Id} not found");

            var success = await _discountRepository.DeleteAsync(discount, cancellationToken);
            if (!success)
                throw new KeyNotFoundException($"Update for Id {request.Id} not successful");

            return new DeleteDiscountResult { Success = true };
        }
    }
}