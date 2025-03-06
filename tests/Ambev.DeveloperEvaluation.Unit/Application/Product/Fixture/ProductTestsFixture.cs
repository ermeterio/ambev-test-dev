using Ambev.DeveloperEvaluation.Application.Products.CreateDiscount;
using Ambev.DeveloperEvaluation.Application.Products.CreateProduct;
using Ambev.DeveloperEvaluation.Application.Products.DeleteDiscount;
using Ambev.DeveloperEvaluation.Application.Products.DeleteProduct;
using Ambev.DeveloperEvaluation.Application.Products.UpdateProduct;
using Ambev.DeveloperEvaluation.Domain.Entities.Product;
using Bogus;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Application.Product.Fixture
{
    [CollectionDefinition(nameof(ProductTestsFixtureCollection))]
    public class ProductTestsFixtureCollection : IClassFixture<ProductTestsFixture>;

    public class ProductTestsFixture
    {
        private readonly Faker _faker = new();

        public CreateProductCommand ValidCreateProductCommandMock()
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

        public UpdateProductCommand InvalidUpdateProductCommandMock()
            => new()
            {
                Id = new Guid(),
                Name = _faker.Vehicle.Model(),
                Description = _faker.Vehicle.Manufacturer(),
                CompanyId = new Guid()
            };

        public DeleteProductCommand ValidDeleteProductCommandMock()
            => new(Guid.NewGuid());

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

        public DeveloperEvaluation.Domain.Entities.Product.Product GetInvalidProductWithNoName()
            => new()
            {
                Description = _faker.Vehicle.Manufacturer(),
                CompanyId = Guid.NewGuid(),
                Price = _faker.Random.Decimal(),
                ActualStock = _faker.Random.Int()
            };

        public DeveloperEvaluation.Domain.Entities.Product.Product GetInvalidProductWithNoDescription()
            => new()
            {
                Name = _faker.Name.FullName(),
                Description = string.Empty,
                CompanyId = Guid.NewGuid(),
                Price = _faker.Random.Decimal(),
                ActualStock = _faker.Random.Int()
            };

        public DeveloperEvaluation.Domain.Entities.Product.Product GetInvalidProductWithNoCompanyId()
            => new()
            {
                Name = _faker.Name.FullName(),
                Description = _faker.Vehicle.Manufacturer(),
                Price = _faker.Random.Decimal(),
                ActualStock = _faker.Random.Int()
            };

        public UpdateProductCommand ValidUpdateProductCommand()
            => new()
            {
                Id = Guid.NewGuid(),
                Name = _faker.Vehicle.Model(),
                CategoryId = Guid.NewGuid(),
                Description = _faker.Vehicle.Manufacturer(),
                CompanyId = Guid.NewGuid(),
                Price = _faker.Random.Decimal(),
                ActualStock = _faker.Random.Int(),
                MaxItemsForSale = 100
            };

        public Discount GetValidDiscount()
            => new ()
            {
                CompanyId = Guid.NewGuid(),
                ProductId = Guid.NewGuid(),
                StartDate = DateTime.Now.AddMonths(-3),
                EndDate = DateTime.Now.AddMonths(3),
                Quantity = 10,
                Value = 100
            };

        public CreateDiscountCommand ValidCreateDiscountCommandMock()
            => new ()
            {
                Quantity = 10,
                Value = 100,
                ProductId = Guid.NewGuid(),
                StartDate = DateTime.Now.AddMonths(-3),
                EndDate = DateTime.Now.AddMonths(3)
            };
        
        public DeleteDiscountCommand ValidDeleteDiscountCommandMock()
            => new(Guid.NewGuid());
    }
}