using Ambev.DeveloperEvaluation.Application.Categories.CreateCategory;
using Bogus;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Application.Category.Fixture
{
    [CollectionDefinition(nameof(CategoryTestsFixtureCollection))]
    public class CategoryTestsFixtureCollection : IClassFixture<CategoryTestsFixture>;

    public class CategoryTestsFixture
    {
        private readonly Faker _faker = new();

        public CreateCategoryCommand ValidCreateCategoryCommandMock()
            => new()
            {
                Name = _faker.Vehicle.Model(),
                CompanyId = Guid.NewGuid()
            };

        public DeveloperEvaluation.Domain.Entities.Product.Category GetValidCategory()
            => new()
            {
                Name = _faker.Vehicle.Model(),
                CompanyId = Guid.NewGuid()
            };
    }
}
