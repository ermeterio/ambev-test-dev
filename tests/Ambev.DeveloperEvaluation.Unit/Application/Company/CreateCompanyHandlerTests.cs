using Ambev.DeveloperEvaluation.Application.Companies.CreateCompany;
using Ambev.DeveloperEvaluation.Unit.Application.Company.Fixture;
using NSubstitute;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Application.Company
{
    public class CreateCompanyHandlerTests
    {
        private readonly CompanyTestsFixture _fixture;
        public CreateCompanyHandlerTests(CompanyTestsFixture fixture)
        {
            _fixture = fixture;
        }

        [Fact(DisplayName = nameof(Should_Be_Create_Successful_Company))]
        [Trait("Company", nameof(CreateCompanyHandlerTests))]
        public async Task Should_Be_Create_Successful_Company()
        {
            //arrange
            var commandHandler = Substitute.For<CreateCompanyHandler>(); 

            //act
            var exceptions = await Record.ExceptionAsync(() => commandHandler.Handle(_fixture.ValidCreateCompanyCommand(), CancellationToken.None));

            //assert
            Assert.Null(exceptions);
        }
    }
}