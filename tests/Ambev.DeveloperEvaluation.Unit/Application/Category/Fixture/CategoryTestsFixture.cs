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

        public CreateCategoryCommand InvalidCreateCategoryCommandMock()
            => new()
            {
                Name = _faker.Vehicle.Model(),
                CompanyId = Guid.NewGuid()
            };

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

        public DeveloperEvaluation.Domain.Entities.Company.Company GetValidCompany()
            => new (_faker.Name.FullName(), null, "84329228000110");
    }
}
