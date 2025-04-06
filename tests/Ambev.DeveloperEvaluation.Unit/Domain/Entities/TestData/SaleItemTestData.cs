using Ambev.DeveloperEvaluation.Domain.Entities;
using System;
using Ambev.DeveloperEvaluation.Domain.Entities.Sale.SaleItem;

namespace Ambev.DeveloperEvaluation.Unit.Domain.Entities.TestData
{
    public static class SaleItemTestData
    {
        public static Ambev.DeveloperEvaluation.Domain.Entities.Sale.SaleItem GenerateValidSaleItem()
        {
            return new SaleItem
            {
                Id = Guid.NewGuid(),
                ProductId = Guid.NewGuid(),
                Quantity = 5
            };
        }

        public static Ambev.DeveloperEvaluation.Domain.Entities.Sale.SaleItem GenerateInvalidSaleItem()
        {
            return new SaleItem
            {
                Id = Guid.NewGuid(),
                ProductId = Guid.Empty, // Invalid: ProductId cannot be empty
                Quantity = 0, // Invalid: Quantity cannot be zero
            };
        }
    }
}