using Ambev.DeveloperEvaluation.Application.Companies.UpdateCompany;
using Ambev.DeveloperEvaluation.Unit.Application.Company.Fixture;
using NSubstitute;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Application.Company
{
    public class UpdateCompanyHandlerTests
    {
        private readonly CompanyTestsFixture _fixture;
        public UpdateCompanyHandlerTests(CompanyTestsFixture fixture)
        {
            _fixture = fixture;
        }

        [Fact(DisplayName = nameof(Should_Be_Update_Successful_Company))]
        [Trait("Company", nameof(UpdateCompanyHandlerTests))]
        public async Task Should_Be_Update_Successful_Company()
        {
            //arrange
            var commandHandler = Substitute.For<UpdateCompanyHandler>();

            //act
            var exceptions = await Record.ExceptionAsync(() => commandHandler.Handle(_fixture.ValidUpdateCompanyCommand(), CancellationToken.None));

            //assert
            Assert.Null(exceptions);
        }
    }
}