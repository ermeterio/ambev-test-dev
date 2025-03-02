using Ambev.DeveloperEvaluation.Application.Sales.CreateSale;
using Ambev.DeveloperEvaluation.Application.Sales.DeleteSale;
using Ambev.DeveloperEvaluation.Application.Sales.UpdateSale;
using Ambev.DeveloperEvaluation.Domain.Entities.Sale;
using Ambev.DeveloperEvaluation.Domain.Enums;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Application.Sale.Fixture
{
    [CollectionDefinition(nameof(SaleTestsFixtureCollection))]
    public class SaleTestsFixtureCollection : ICollectionFixture<SaleTestsFixture>;
    public class SaleTestsFixture
    {
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
        
        public DeleteSaleCommand ValidDeleteSaleCommandMock()
            => new ()
                {
                    Id = Guid.NewGuid()
                };

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
    }
}