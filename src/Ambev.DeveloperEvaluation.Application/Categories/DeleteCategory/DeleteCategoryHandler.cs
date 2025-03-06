using Ambev.DeveloperEvaluation.Domain.Repositories;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Ambev.DeveloperEvaluation.Application.Categories.DeleteCategory
{
    public class DeleteCategoryHandler : IRequestHandler<DeleteCategoryCommand, DeleteCategoryResult>
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly ILogger<DeleteCategoryHandler> _logger;
        private readonly IProductRepository _productRepository;

        public DeleteCategoryHandler(ICategoryRepository categoryRepository, IProductRepository productRepository, ILogger<DeleteCategoryHandler> logger)
        {
            _categoryRepository = categoryRepository;
            _productRepository = productRepository;
            _logger = logger;
        }

        public async Task<DeleteCategoryResult> Handle(DeleteCategoryCommand request, CancellationToken cancellationToken)
        {
            var validator = new DeleteCategoryValidator();
            var validationResult = await validator.ValidateAsync(request, cancellationToken);
            if (!validationResult.IsValid)
                throw new ValidationException(validationResult.Errors);

            var category = await _categoryRepository.GetByIdAsync(request.Id) ??
                           throw new InvalidOperationException("Category not found");

            var productsFromCategory = await _productRepository.ExistsProductCategoryAsync(request.Id, cancellationToken);
            if (productsFromCategory)
                throw new InvalidOperationException("Category has products");

            _logger.LogInformation("Deleting category {@Category}", category);

            await _categoryRepository.DeleteAsync(category, cancellationToken);

            return new();
        }
    }
}