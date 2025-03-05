using Ambev.DeveloperEvaluation.Application.Companies.CreateCompany;
using Ambev.DeveloperEvaluation.Application.Companies.UpdateCompany;
using Bogus;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Application.Company.Fixture
{
    [CollectionDefinition(nameof(CompanyTestsFixtureCollection))]
    public class CompanyTestsFixtureCollection : IClassFixture<CompanyTestsFixture>;

    public class CompanyTestsFixture
    {
        private readonly Faker _faker;
        public CompanyTestsFixture()
        {
            _faker = new Faker();
        }

        public CreateCompanyCommand ValidCreateCompanyCommand()
        {
            return new CreateCompanyCommand
            {
                Name = _faker.Name.FullName(),
                Cnpj = "84329228000110"
            };
        }

        public CreateCompanyCommand InvalidCreateCompanyCommandWithNoName()
        {
            return new CreateCompanyCommand
            {
                Name = string.Empty,
                Cnpj = "84329228000110"
            };
        }

        public UpdateCompanyCommand ValidUpdateCompanyCommand()
        {
            return new UpdateCompanyCommand
            {
                Id = Guid.NewGuid(),
                Name = _faker.Name.FullName(),
                Cnpj = "84329228000110"
            };
        }

        public DeveloperEvaluation.Domain.Entities.Company.Company GetValidCompany()
            => new (_faker.Name.FullName(), null, "84329228000110");

        public DeveloperEvaluation.Domain.Entities.Company.Company GetInvalidCompany()
            => new (_faker.Name.FullName(), null, "123456");

        public DeveloperEvaluation.Domain.Entities.Company.Company GetInvalidCompanyWithNotName()
            => new(string.Empty, null, "84329228000110");

        public DeveloperEvaluation.Domain.Entities.Company.Company GetInvalidCompanyWithInvalidCnpj()
            => new(string.Empty, null, "123456");

    }
}