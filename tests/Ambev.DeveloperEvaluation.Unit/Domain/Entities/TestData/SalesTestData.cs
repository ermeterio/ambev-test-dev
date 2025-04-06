using Ambev.DeveloperEvaluation.Domain.Entities.Company;
using Ambev.DeveloperEvaluation.Domain.Entities.Sale;
using Ambev.DeveloperEvaluation.Domain.Enums;
using Sale = Ambev.DeveloperEvaluation.Domain.Entities.Sale.Sale;

namespace Ambev.DeveloperEvaluation.Unit.Domain.Entities.TestData
{
    public static class SalesTestData
    {
        public static DeveloperEvaluation.Domain.Entities.Sale.Sale GenerateValidSale()
        {
            return new DeveloperEvaluation.Domain.Entities.Sale.Sale
            {
                Id = Guid.NewGuid(),
                UserId = Guid.NewGuid(),
                Company = new Company { Id = Guid.NewGuid(), Name = "Valid Company", Cnpj = "12345678000195" },
                User = new DeveloperEvaluation.Domain.Entities.User.User { Id = Guid.NewGuid(), Username = "validuser", Email = "validuser@example.com" },
                Discounts = new List<SaleDiscount>(),
                Items = new List<SaleItem>
                {
                    new SaleItem
                    {
                        Id = Guid.NewGuid(),
                        ProductId = Guid.NewGuid(),
                        Quantity = 5
                    }
                },
                Status = SaleStatus.Processing
            };
        }

        public static DeveloperEvaluation.Domain.Entities.Sale.Sale GenerateInvalidSale()
        {
            return new DeveloperEvaluation.Domain.Entities.Sale.Sale
            {
                Id = Guid.NewGuid(),
                UserId = Guid.Empty, // Invalid: UserId cannot be empty
                Company = null, // Invalid: Company cannot be null
                User = null, // Invalid: User cannot be null
                Discounts = null,
                Items = null, // Invalid: Items cannot be null or empty
                Status = SaleStatus.Started // Invalid: Status cannot be Unknown
            };
        }

        public static DeveloperEvaluation.Domain.Entities.Sale.Sale GenerateSaleWithMultipleItems()
        {
            return new DeveloperEvaluation.Domain.Entities.Sale.Sale
            {
                Id = Guid.NewGuid(),
                UserId = Guid.NewGuid(),
                Company = new Company { Id = Guid.NewGuid(), Name = "Valid Company", Cnpj = "12345678000195" },
                User = new DeveloperEvaluation.Domain.Entities.User.User { Id = Guid.NewGuid(), Username = "validuser", Email = "validuser@example.com" },
                Discounts = new List<SaleDiscount>(),
                Items = new List<SaleItem>
                {
                    new SaleItem
                    {
                        Id = Guid.NewGuid(),
                        ProductId = Guid.NewGuid(),
                        Quantity = 5
                    },
                    new SaleItem
                    {
                        Id = Guid.NewGuid(),
                        ProductId = Guid.NewGuid(),
                        Quantity = 15
                    },
                    new SaleItem
                    {
                        Id = Guid.NewGuid(),
                        ProductId = Guid.NewGuid(),
                        Quantity = 2
                    }
                },
                Status = SaleStatus.Processing
            };
        }
    }
}
