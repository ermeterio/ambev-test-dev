using Ambev.DeveloperEvaluation.Application.Companies.CreateCompany;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Ambev.DeveloperEvaluation.Unit.Application.Company.Fixture;
using AutoMapper;
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
            var commandHandler = new CreateCompanyHandler(companyRepository, Substitute.For<IMapper>());

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
            var commandHandler = new CreateCompanyHandler(companyRepository, Substitute.For<IMapper>());

            //act
            var exceptions = await Record.ExceptionAsync(() => commandHandler.Handle(_fixture.ValidCreateCompanyCommand(), CancellationToken.None));

            //assert
            Assert.NotNull(exceptions);
        }

        [Fact(DisplayName = nameof(Should_Be_Update_Invalid_Company))]
        [Trait("Company", nameof(CreateCompanyHandlerTests))]
        public async Task Should_Be_Update_Invalid_Company()
        {
            //arrange
            var company = _fixture.GetInvalidCompany();
            var companyRepository = Substitute.For<ICompanyRepository>();
            companyRepository.GetByCnpjAsync(company.Cnpj, CancellationToken.None).Returns(company);
            var commandHandler = new CreateCompanyHandler(companyRepository, Substitute.For<IMapper>());

            //act
            var exceptions = await Record.ExceptionAsync(() => commandHandler.Handle(_fixture.ValidCreateCompanyCommand(), CancellationToken.None));

            //assert
            Assert.NotNull(exceptions);
        }
        #endregion
    }
}