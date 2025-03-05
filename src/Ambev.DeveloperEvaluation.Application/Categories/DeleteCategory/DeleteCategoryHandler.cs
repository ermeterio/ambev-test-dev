using Ambev.DeveloperEvaluation.Domain.Repositories;
using FluentValidation;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Categories.DeleteCategory
{
    public class DeleteCategoryHandler : IRequestHandler<DeleteCategoryCommand, DeleteCategoryResult>
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IProductRepository _productRepository;

        public DeleteCategoryHandler(ICategoryRepository categoryRepository, IProductRepository productRepository)
        {
            _categoryRepository = categoryRepository;
            _productRepository = productRepository;
        }

        public async Task<DeleteCategoryResult> Handle(DeleteCategoryCommand request, CancellationToken cancellationToken)
        {
            var validator = new DeleteCategoryValidator();
            var validationResult = await validator.ValidateAsync(request, cancellationToken);
            if (!validationResult.IsValid)
                throw new ValidationException(validationResult.Errors);

            var category = await _categoryRepository.GetByIdAsync(request.Id);
            if (category is null)
                throw new InvalidOperationException("Category not found");

            var productsFromCategory = await _productRepository.ExistsProductCategoryAsync(request.Id, cancellationToken);
            if (productsFromCategory)
                throw new InvalidOperationException("Category has products");

            await _categoryRepository.DeleteAsync(category, cancellationToken);

            return new();
        }
    }
}