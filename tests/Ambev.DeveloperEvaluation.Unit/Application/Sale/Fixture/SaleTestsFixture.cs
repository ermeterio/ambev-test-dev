using Ambev.DeveloperEvaluation.Application.Sales.CreateSale;
using Ambev.DeveloperEvaluation.Application.Sales.DeleteSale;
using Ambev.DeveloperEvaluation.Application.Sales.UpdateSale;
using Ambev.DeveloperEvaluation.Domain.Entities.Sale;
using Ambev.DeveloperEvaluation.Domain.Enums;
using Bogus;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Application.Sale.Fixture
{
    [CollectionDefinition(nameof(SaleTestsFixtureCollection))]
    public class SaleTestsFixtureCollection : IClassFixture<SaleTestsFixture>;
    public class SaleTestsFixture
    {
        private readonly Faker _faker = new();
        public CreateSaleCommand ValidCreateSaleCommandMock()
            => new ()
                {
                    CompanyId = Guid.NewGuid(),
                    Items = GetSaleItemsMock(),
                    Discounts = Enumerable.Empty<SaleDiscount>(),
                    Status = SaleStatus.Started,
                    UserId = Guid.NewGuid()
                };

        public IEnumerable<SaleItem> GetSaleItemsMock()
            => [new SaleItem { ProductId = Guid.NewGuid(), Quantity = 1 }];

        public DeveloperEvaluation.Domain.Entities.Product.Product GetValidProduct()
            => new()
            {
                Name = _faker.Vehicle.Model(),
                CategoryId = Guid.NewGuid(),
                Description = _faker.Vehicle.Manufacturer(),
                CompanyId = Guid.NewGuid(),
                Price = _faker.Random.Decimal(),
                ActualStock = _faker.Random.Int(),
                MaxItemsForSale = 100
            };
        public DeleteSaleCommand ValidDeleteSaleCommandMock()
            => new(Guid.NewGuid());

        public UpdateSaleCommand ValidUpdateSaleCommandMock()
            => new ()
            {
                Id = Guid.NewGuid(),
                CompanyId = Guid.NewGuid(),
                Items = GetSaleItemsMock(),
                Discounts = Enumerable.Empty<SaleDiscount>(),
                Status = SaleStatus.Started,
                UserId = Guid.NewGuid()
            };

        public DeveloperEvaluation.Domain.Entities.Sale.Sale GetValidSale()
            => new ()
            {
                CompanyId = Guid.NewGuid(),
                Items = GetSaleItemsMock(),
                Discounts = Enumerable.Empty<SaleDiscount>(),
                Status = SaleStatus.Started,
                UserId = Guid.NewGuid()
            };

        public DeveloperEvaluation.Domain.Entities.Sale.Sale GetInvalidSale()
            => new ()
            {
                Discounts = Enumerable.Empty<SaleDiscount>(),
                Status = SaleStatus.Started,
                UserId = Guid.NewGuid()
            };

        public DeveloperEvaluation.Domain.Entities.Sale.Sale GetInvalidSaleWithoutItems()
            => new ()
            {
                CompanyId = Guid.NewGuid(),
                Items = Enumerable.Empty<SaleItem>(),
                Discounts = Enumerable.Empty<SaleDiscount>(),
                Status = SaleStatus.Started,
                UserId = Guid.NewGuid()
            };
    }
}