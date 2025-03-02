using Ambev.DeveloperEvaluation.Application.Sales.CreateSale;
using Ambev.DeveloperEvaluation.Unit.Application.Sale.Fixture;
using NSubstitute;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Application.Sale
{
    public class CreateSaleHandlerTests
    {
        private readonly SaleTestsFixture _fixture;
        public CreateSaleHandlerTests(SaleTestsFixture fixture)
        {
            _fixture = fixture;
        }

        [Fact(DisplayName = nameof(Should_Be_Create_Sale_Product))]
        [Trait("Sale", nameof(CreateSaleHandlerTests))]
        public async Task Should_Be_Create_Sale_Product()
        {
            //arrange
            var commandHandler = Substitute.For<CreateSaleHandler>();

            //act
            var exceptions = await Record.ExceptionAsync(() => commandHandler.Handle(_fixture.ValidCreateSaleCommandMock(), CancellationToken.None));

            //assert
            Assert.Null(exceptions);
        }
    }
}
