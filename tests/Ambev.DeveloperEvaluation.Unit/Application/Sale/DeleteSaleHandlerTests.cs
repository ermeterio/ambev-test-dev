using Ambev.DeveloperEvaluation.Application.Sales.DeleteSale;
using Ambev.DeveloperEvaluation.Unit.Application.Sale.Fixture;
using NSubstitute;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Application.Sale
{
    public class DeleteSaleHandlerTests
    {
        private readonly SaleTestsFixture _fixture;
        public DeleteSaleHandlerTests(SaleTestsFixture fixture)
        {
            _fixture = fixture;
        }

        [Fact(DisplayName = nameof(Should_Be_Delete_Sale_Product))]
        [Trait("Sale", nameof(DeleteSaleHandlerTests))]
        public async Task Should_Be_Delete_Sale_Product()
        {
            //arrange
            var commandHandler = Substitute.For<DeleteSaleHandler>();

            //act
            var exceptions = await Record.ExceptionAsync(() => commandHandler.Handle(_fixture.ValidDeleteSaleCommandMock(), CancellationToken.None));

            //assert
            Assert.Null(exceptions);
        }
    }
}
