using Ambev.DeveloperEvaluation.Application.Products.DeleteProduct;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Ambev.DeveloperEvaluation.Unit.Application.Product.Fixture;
using NSubstitute;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Application.Product
{
    [Collection(nameof(ProductTestsFixtureCollection))]
    public class DeleteProductHandlerTests
    {
        private readonly ProductTestsFixture _fixture;
        public DeleteProductHandlerTests(ProductTestsFixture fixture)
        {
            _fixture = fixture;
        }
        #region Success
        [Fact(DisplayName = nameof(Should_Be_Delete_Successful_Product))]
        [Trait("Product", nameof(DeleteProductHandlerTests))]
        public async Task Should_Be_Delete_Successful_Product()
        {
            //arrange
            var productRepository = Substitute.For<IProductRepository>();
            productRepository.GetByIdAsync(Arg.Any<Guid>()).Returns(_fixture.GetValidProduct());
            productRepository.DeleteAsync(Arg.Any<DeveloperEvaluation.Domain.Entities.Product.Product>(), Arg.Any<CancellationToken>()).Returns(true);
            var commandHandler = new DeleteProductHandler(productRepository);

            //act
            var exceptions = await Record.ExceptionAsync(() => commandHandler.Handle(_fixture.ValidDeleteProductCommandMock(), CancellationToken.None));

            //assert
            Assert.Null(exceptions);
        }
        #endregion
        #region Error
        [Fact(DisplayName = nameof(Should_Be_Delete_Not_Found_Product))]
        [Trait("Product", nameof(DeleteProductHandlerTests))]
        public async Task Should_Be_Delete_Not_Found_Product()
        {
            //arrange
            var productRepository = Substitute.For<IProductRepository>();
            productRepository.GetByIdAsync(Arg.Any<Guid>()).Returns(_fixture.GetValidProduct());
            var commandHandler = new DeleteProductHandler(productRepository);

            //act
            var exceptions = await Record.ExceptionAsync(() => commandHandler.Handle(new DeleteProductCommand(new Guid()), CancellationToken.None));

            //assert
            Assert.NotNull(exceptions);
        }
        #endregion
    }
}