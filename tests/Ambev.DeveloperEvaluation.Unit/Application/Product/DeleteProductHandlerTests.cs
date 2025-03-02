using Ambev.DeveloperEvaluation.Application.Products.DeleteProduct;
using Ambev.DeveloperEvaluation.Unit.Application.Product.Fixture;
using NSubstitute;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Application.Product
{
    public class DeleteProductHandlerTests
    {
        private readonly ProductTestsFixture _fixture;
        public DeleteProductHandlerTests(ProductTestsFixture fixture)
        {
            _fixture = fixture;
        }

        [Fact(DisplayName = nameof(Should_Be_Delete_Successful_Product))]
        [Trait("Product", nameof(DeleteProductHandlerTests))]
        public async Task Should_Be_Delete_Successful_Product()
        {
            //arrange
            var commandHandler = Substitute.For<DeleteProductHandler>();

            //act
            var exceptions = await Record.ExceptionAsync(() => commandHandler.Handle(_fixture.ValidDeleteProductCommandMock(), CancellationToken.None));

            //assert
            Assert.Null(exceptions);
        }
    }
}
