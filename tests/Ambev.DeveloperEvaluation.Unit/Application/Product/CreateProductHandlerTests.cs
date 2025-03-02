using Ambev.DeveloperEvaluation.Application.Products.CreateProduct;
using Ambev.DeveloperEvaluation.Unit.Application.Product.Fixture;
using NSubstitute;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Application.Product
{
    public class CreateProductHandlerTests
    {
        private readonly ProductTestsFixture _fixture;
        public CreateProductHandlerTests(ProductTestsFixture fixture)
        {
            _fixture = fixture;
        }

        [Fact(DisplayName = nameof(Should_Be_Create_Successful_Product))]
        [Trait("Product", nameof(CreateProductHandlerTests))]
        public async Task Should_Be_Create_Successful_Product()
        {
            //arrange
            var commandHandler = Substitute.For<CreateProductHandler>();

            //act
            var exceptions = await Record.ExceptionAsync(() => commandHandler.Handle(_fixture.ValidCreateProductCommandMock(), CancellationToken.None));

            //assert
            Assert.Null(exceptions);
        }
    }
}
