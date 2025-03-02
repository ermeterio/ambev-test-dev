using Ambev.DeveloperEvaluation.Application.Companies.DeleteCompany;
using Ambev.DeveloperEvaluation.Unit.Application.Company.Fixture;
using NSubstitute;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Application.Company
{
    public class DeleteCompanyHandlerTests
    {
        private readonly CompanyTestsFixture _fixture;
        public DeleteCompanyHandlerTests(CompanyTestsFixture fixture)
        {
            _fixture = fixture;
        }

        [Fact(DisplayName = nameof(Should_Be_Delete_Successful_Company))]
        [Trait("Company", nameof(DeleteCompanyHandlerTests))]
        public async Task Should_Be_Delete_Successful_Company()
        {
            //arrange
            var commandHandler = Substitute.For<DeleteCompanyHandler>();

            //act
            var exceptions = await Record.ExceptionAsync(() => commandHandler.Handle(new DeleteCompanyCommand{Id = new Guid()}, CancellationToken.None));

            //assert
            Assert.Null(exceptions);
        }
    }
}
