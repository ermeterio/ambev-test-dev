using Ambev.DeveloperEvaluation.Application.Categories.CreateCategory;
using Ambev.DeveloperEvaluation.Application.Companies.CreateCompany;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Ambev.DeveloperEvaluation.Unit.Application.Company.Fixture;
using AutoMapper;
using Bogus;
using Microsoft.Extensions.Logging;
using NSubstitute;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Application.Company
{
    [Collection(nameof(CompanyTestsFixtureCollection))]
    public class CreateCompanyHandlerTests
    {
        private readonly CompanyTestsFixture _fixture;
        public CreateCompanyHandlerTests(CompanyTestsFixture fixture)
        {
            _fixture = fixture;
        }
        #region Success
        [Fact(DisplayName = nameof(Should_Be_Create_Successful_Company))]
        [Trait("Company", nameof(CreateCompanyHandlerTests))]
        public async Task Should_Be_Create_Successful_Company()
        {
            //arrange

            var companyRepository = Substitute.For<ICompanyRepository>();
            companyRepository.AddAsync(Arg.Any<DeveloperEvaluation.Domain.Entities.Company.Company>()).Returns(_fixture.GetValidCompany());
            var commandHandler = new CreateCompanyHandler(companyRepository, Substitute.For<IMapper>(), Substitute.For<ILogger<CreateCompanyHandler>>());

            //act
            var exceptions = await Record.ExceptionAsync(() => commandHandler.Handle(_fixture.ValidCreateCompanyCommand(), CancellationToken.None));

            //assert
            Assert.Null(exceptions);
        }
        #endregion

        #region Error
        [Fact(DisplayName = nameof(Should_Be_Create_Existing_Company))]
        [Trait("Company", nameof(CreateCompanyHandlerTests))]
        public async Task Should_Be_Create_Existing_Company()
        {
            //arrange
            var company = _fixture.GetValidCompany();
            var companyRepository = Substitute.For<ICompanyRepository>();
            companyRepository.GetByCnpjAsync(company.Cnpj, CancellationToken.None).Returns(company);
            var commandHandler = new CreateCompanyHandler(companyRepository, Substitute.For<IMapper>(), Substitute.For<ILogger<CreateCompanyHandler>>());

            //act
            var exceptions = await Record.ExceptionAsync(() => commandHandler.Handle(_fixture.ValidCreateCompanyCommand(), CancellationToken.None));

            //assert
            Assert.NotNull(exceptions);
        }

        [Fact(DisplayName = nameof(Should_Be_Create_Invalid_Company_With_No_Name))]
        [Trait("Company", nameof(CreateCompanyHandlerTests))]
        public async Task Should_Be_Create_Invalid_Company_With_No_Name()
        {
            //arrange
            var company = _fixture.GetInvalidCompanyWithNotName();
            var companyRepository = Substitute.For<ICompanyRepository>();
            companyRepository.GetByCnpjAsync(company.Cnpj, CancellationToken.None).Returns(company);
            var commandHandler = new CreateCompanyHandler(companyRepository, Substitute.For<IMapper>(), Substitute.For<ILogger<CreateCompanyHandler>>());

            //act
            var exceptions = await Record.ExceptionAsync(() => commandHandler.Handle(_fixture.ValidCreateCompanyCommand(), CancellationToken.None));

            //assert
            Assert.NotNull(exceptions);
        }

        [Fact(DisplayName = nameof(Should_Be_Create_Invalid_Company_With_Invalid_Cnpj))]
        [Trait("Company", nameof(CreateCompanyHandlerTests))]
        public async Task Should_Be_Create_Invalid_Company_With_Invalid_Cnpj()
        {
            //arrange
            var company = _fixture.GetInvalidCompanyWithInvalidCnpj();
            var companyRepository = Substitute.For<ICompanyRepository>();
            companyRepository.GetByCnpjAsync(company.Cnpj, CancellationToken.None).Returns(company);
            var commandHandler = new CreateCompanyHandler(companyRepository, Substitute.For<IMapper>(), Substitute.For<ILogger<CreateCompanyHandler>>());

            //act
            var exceptions = await Record.ExceptionAsync(() => commandHandler.Handle(_fixture.InvalidCreateCompanyCommandWithNoName(), CancellationToken.None));

            //assert
            Assert.NotNull(exceptions);
        }
        #endregion
    }
}