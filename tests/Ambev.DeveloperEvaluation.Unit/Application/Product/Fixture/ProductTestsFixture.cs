using Ambev.DeveloperEvaluation.Application.Products.CreateProduct;
using Ambev.DeveloperEvaluation.Application.Products.DeleteProduct;
using Ambev.DeveloperEvaluation.Application.Products.UpdateProduct;
using Bogus;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Application.Product.Fixture
{
    [CollectionDefinition(nameof(ProductTestsFixtureCollection))]
    public class ProductTestsFixtureCollection : ICollectionFixture<ProductTestsFixture>;

    public class ProductTestsFixture
    {
        private readonly Faker _faker = new ("pt-BR");
        public CreateProductCommand ValidCreateProductCommandMock()
            => new ()
                {
                    Name = _faker.Vehicle.Model(),
                    Description = _faker.Vehicle.Manufacturer(),
                    CompanyId = new Guid()
                };
        public UpdateProductCommand ValidUpdateProductCommandMock()
            => new ()
                {
                    Id = new Guid(),
                    Name = _faker.Vehicle.Model(),
                    Description = _faker.Vehicle.Manufacturer(),
                    CompanyId = new Guid()
                };
        public DeleteProductCommand ValidDeleteProductCommandMock()
            => new ()
                {
                    Id = Guid.NewGuid()
                };
    }
}