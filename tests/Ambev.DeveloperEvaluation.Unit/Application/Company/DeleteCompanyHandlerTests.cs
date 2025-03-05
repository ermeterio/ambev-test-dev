using Ambev.DeveloperEvaluation.Application.Companies.DeleteCompany;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Ambev.DeveloperEvaluation.Unit.Application.Company.Fixture;
using NSubstitute;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Application.Company
{
    [Collection(nameof(CompanyTestsFixtureCollection))]
    public class DeleteCompanyHandlerTests
    {
        private readonly CompanyTestsFixture _fixture;
        public DeleteCompanyHandlerTests(CompanyTestsFixture fixture)
        {
            _fixture = fixture;
        }
        #region Success
        [Fact(DisplayName = nameof(Should_Be_Delete_Successful_Company))]
        [Trait("Company", nameof(DeleteCompanyHandlerTests))]
        public async Task Should_Be_Delete_Successful_Company()
        {
            //arrange
            var companyRepository = Substitute.For<ICompanyRepository>();
            companyRepository.GetByIdAsync(Arg.Any<Guid>()).Returns(_fixture.GetValidCompany());
            companyRepository.DeleteAsync(Arg.Any<DeveloperEvaluation.Domain.Entities.Company.Company>())
                .Returns(true);
            var commandHandler = new DeleteCompanyHandler(companyRepository);

            //act
            var exceptions = await Record.ExceptionAsync(() => commandHandler.Handle(new DeleteCompanyCommand(Guid.NewGuid()), CancellationToken.None));

            //assert
            Assert.Null(exceptions);
        }
        #endregion

        #region Error
        [Fact(DisplayName = nameof(Should_Be_Delete_Not_Found_Company))]
        [Trait("Company", nameof(DeleteCompanyHandlerTests))]
        public async Task Should_Be_Delete_Not_Found_Company()
        {
            //arrange
            var companyRepository = Substitute.For<ICompanyRepository>();
            companyRepository.GetByIdAsync(Arg.Any<Guid>()).Returns(_fixture.GetValidCompany());
            var commandHandler = new DeleteCompanyHandler(companyRepository);

            //act
            var exceptions = await Record.ExceptionAsync(() => commandHandler.Handle(new DeleteCompanyCommand(new Guid()), CancellationToken.None));

            //assert
            Assert.NotNull(exceptions);
        }
        #endregion
    }
}