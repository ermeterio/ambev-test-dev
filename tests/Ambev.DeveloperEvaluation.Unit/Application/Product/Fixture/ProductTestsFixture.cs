using Ambev.DeveloperEvaluation.Application.Products.CreateProduct;
using Ambev.DeveloperEvaluation.Application.Products.DeleteProduct;
using Ambev.DeveloperEvaluation.Application.Products.UpdateProduct;
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
                ActualStock = _faker.Random.Int()
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
                ActualStock = _faker.Random.Int()
            };

        public DeveloperEvaluation.Domain.Entities.Product.Product GetInvalidProduct()
            => new()
            {
                Description = _faker.Vehicle.Manufacturer(),
                CompanyId = Guid.NewGuid(),
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
                ActualStock = _faker.Random.Int()
            };
    }
}