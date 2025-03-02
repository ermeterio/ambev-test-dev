﻿using Ambev.DeveloperEvaluation.Domain.Entities.Product;

namespace Ambev.DeveloperEvaluation.Domain.Repositories
{
    public interface IProductRepository : IRepository<Product>
    {
        Task<Product?> GetByNameAndCategoryAsync(string name, Guid categoryId, CancellationToken cancellationToken);
    }
}