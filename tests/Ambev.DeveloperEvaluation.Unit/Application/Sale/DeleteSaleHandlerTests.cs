using Ambev.DeveloperEvaluation.Application.Sales.DeleteSale;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Ambev.DeveloperEvaluation.Unit.Application.Sale.Fixture;
using NSubstitute;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Application.Sale
{
    [Collection(nameof(SaleTestsFixtureCollection))]
    public class DeleteSaleHandlerTests
    {
        private readonly SaleTestsFixture _fixture;
        public DeleteSaleHandlerTests(SaleTestsFixture fixture)
        {
            _fixture = fixture;
        }
        #region Success
        [Fact(DisplayName = nameof(Should_Be_Delete_Sale_Sale))]
        [Trait("Sale", nameof(DeleteSaleHandlerTests))]
        public async Task Should_Be_Delete_Sale_Sale()
        {
            //arrange
            var saleRepository = Substitute.For<ISaleRepository>();
            saleRepository.GetByIdAsync(Arg.Any<Guid>()).Returns(_fixture.GetValidSale());
            saleRepository.DeleteAsync(Arg.Any<DeveloperEvaluation.Domain.Entities.Sale.Sale>()).Returns(true);
            var commandHandler = new DeleteSaleHandler(saleRepository);

            //act
            var exceptions = await Record.ExceptionAsync(() => commandHandler.Handle(_fixture.ValidDeleteSaleCommandMock(), CancellationToken.None));

            //assert
            Assert.Null(exceptions);
        }
        #endregion
        #region Error
        [Fact(DisplayName = nameof(Should_Be_Delete_Not_Found_Sale))]
        [Trait("Sale", nameof(DeleteSaleHandlerTests))]
        public async Task Should_Be_Delete_Not_Found_Sale()
        {
            //arrange
            var saleRepository = Substitute.For<ISaleRepository>();
            var commandHandler = new DeleteSaleHandler(saleRepository);
            saleRepository.GetByIdAsync(Arg.Any<Guid>()).Returns(_fixture.GetValidSale());

            //act
            var exceptions = await Record.ExceptionAsync(() => commandHandler.Handle(new DeleteSaleCommand(new Guid()), CancellationToken.None));

            //assert
            Assert.NotNull(exceptions);
        }
        #endregion
    }
}
