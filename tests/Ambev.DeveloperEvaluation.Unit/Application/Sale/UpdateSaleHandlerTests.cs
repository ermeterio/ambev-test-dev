using Ambev.DeveloperEvaluation.Application.Sales.UpdateSale;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Ambev.DeveloperEvaluation.Unit.Application.Sale.Fixture;
using AutoMapper;
using NSubstitute;
using NSubstitute.ReturnsExtensions;
using Rebus.Bus;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Application.Sale
{
    [Collection(nameof(SaleTestsFixtureCollection))]
    public class UpdateSaleHandlerTests
    {
        private readonly SaleTestsFixture _fixture;
        public UpdateSaleHandlerTests(SaleTestsFixture fixture)
        {
            _fixture = fixture;
        }
        #region Success
        [Fact(DisplayName = nameof(Should_Be_Update_Sale_Sale))]
        [Trait("Sale", nameof(UpdateSaleHandlerTests))]
        public async Task Should_Be_Update_Sale_Sale()
        {
            //arrange
            var sale = _fixture.GetValidSale();

            var saleRepository = Substitute.For<ISaleRepository>();
            saleRepository.GetByIdAsync(Arg.Any<Guid>()).Returns(sale);
            saleRepository.UpdateAsync(Arg.Any<DeveloperEvaluation.Domain.Entities.Sale.Sale>()).Returns(sale);

            var mapper = Substitute.For<IMapper>();
            mapper.Map<DeveloperEvaluation.Domain.Entities.Sale.Sale>(Arg.Any<UpdateSaleCommand>())
                .Returns(sale);

            var commandHandler = new UpdateSaleHandler(saleRepository, mapper, 
                Substitute.For<IProductRepository>(), Substitute.For<IBus>());
            
            //act
            var exceptions = await Record.ExceptionAsync(() => commandHandler.Handle(_fixture.ValidUpdateSaleCommandMock(), CancellationToken.None));

            //assert
            Assert.Null(exceptions);
        }
        #endregion
        #region Error
        [Fact(DisplayName = nameof(Should_Be_Unsuccessful_Update_Not_Found_Sale))]
        [Trait("Sale", nameof(UpdateSaleHandlerTests))]
        public async Task Should_Be_Unsuccessful_Update_Not_Found_Sale()
        {
            //arrange
            var repository = Substitute.For<ISaleRepository>();
            repository.GetByIdAsync(Arg.Any<Guid>()).ReturnsNull();
            var commandHandler = new UpdateSaleHandler(repository, Substitute.For<IMapper>(), Substitute.For<IProductRepository>(), Substitute.For<IBus>());

            //act
            var exceptions = await Record.ExceptionAsync(() => commandHandler.Handle(_fixture.ValidUpdateSaleCommandMock(), CancellationToken.None));

            //assert
            Assert.NotNull(exceptions);
        }

        [Fact(DisplayName = nameof(Should_Be_Update_Sale_With_Invalid_Sale))]
        [Trait("Sale", nameof(UpdateSaleHandlerTests))]
        public async Task Should_Be_Update_Sale_With_Invalid_Sale()
        {
            //arrange
            var sale = _fixture.GetInvalidSale();
            var saleRepository = Substitute.For<ISaleRepository>();
            saleRepository.GetExistingSaleForUser(Arg.Any<Guid>(), CancellationToken.None).Returns(sale);
            var commandHandler = new UpdateSaleHandler(saleRepository, Substitute.For<IMapper>(), Substitute.For<IProductRepository>(), Substitute.For<IBus>());

            //act
            var exceptions = await Record.ExceptionAsync(() => commandHandler.Handle(_fixture.ValidUpdateSaleCommandMock(), CancellationToken.None));

            //assert
            Assert.NotNull(exceptions);
        }
        #endregion
    }
}
