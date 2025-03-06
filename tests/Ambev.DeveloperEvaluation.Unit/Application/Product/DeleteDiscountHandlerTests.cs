using Ambev.DeveloperEvaluation.Application.Products.CreateProduct;
using Ambev.DeveloperEvaluation.Application.Products.DeleteDiscount;
using Ambev.DeveloperEvaluation.Domain.Entities.Product;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Ambev.DeveloperEvaluation.Unit.Application.Product.Fixture;
using Microsoft.Extensions.Logging;
using NSubstitute;
using NSubstitute.ReturnsExtensions;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Application.Product
{
    [Collection(nameof(ProductTestsFixtureCollection))]
    public class DeleteDiscountHandlerTests
    {
        private readonly ProductTestsFixture _fixture;
        public DeleteDiscountHandlerTests(ProductTestsFixture fixture)
        {
            _fixture = fixture;
        }

        #region Success
        [Fact(DisplayName = nameof(Should_Be_Delete_Successful_Discount))]
        [Trait("Discount", nameof(DeleteDiscountHandlerTests))]
        public async Task Should_Be_Delete_Successful_Discount()
        {
            //arrange
            var discountRepository = Substitute.For<IDiscountRepository>();
            discountRepository.GetByIdAsync(Arg.Any<Guid>()).Returns(_fixture.GetValidDiscount());
            discountRepository.DeleteAsync(Arg.Any<Discount>(), Arg.Any<CancellationToken>()).Returns(true);
            var commandHandler = new DeleteDiscountHandler(discountRepository, Substitute.For<ILogger<DeleteDiscountHandler>>());

            //act
            var exceptions = await Record.ExceptionAsync(() => commandHandler.Handle(_fixture.ValidDeleteDiscountCommandMock(), CancellationToken.None));

            //assert
            Assert.Null(exceptions);
        }
        #endregion

        #region Error
        [Fact(DisplayName = nameof(Should_Be_Delete_Not_Found_Discount))]
        [Trait("Discount", nameof(DeleteDiscountHandlerTests))]
        public async Task Should_Be_Delete_Not_Found_Discount()
        {
            //arrange
            var discountRepository = Substitute.For<IDiscountRepository>();
            discountRepository.GetByIdAsync(Arg.Any<Guid>()).ReturnsNull();
            discountRepository.DeleteAsync(Arg.Any<Discount>(), Arg.Any<CancellationToken>()).Returns(true);
            var commandHandler = new DeleteDiscountHandler(discountRepository, Substitute.For<ILogger<DeleteDiscountHandler>>());

            //act
            var exceptions = await Record.ExceptionAsync(() => commandHandler.Handle(_fixture.ValidDeleteDiscountCommandMock(), CancellationToken.None));

            //assert
            Assert.NotNull(exceptions);
        }
        #endregion
    }
}