using Ambev.DeveloperEvaluation.Application.Sales.CreateSale;
using Ambev.DeveloperEvaluation.Application.Sales.DeleteSale;
using Ambev.DeveloperEvaluation.Application.Sales.UpdateSale;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Ambev.DeveloperEvaluation.ORM.Repositories;
using Ambev.DeveloperEvaluation.Unit.Application.Sale.Fixture;
using AutoMapper;
using Microsoft.Extensions.Logging;
using NSubstitute;
using NSubstitute.ReturnsExtensions;
using Rebus.Bus;
using Rebus.Compression;
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

            var companyRepository = Substitute.For<ICompanyRepository>();
            companyRepository.GetByIdAsync(Arg.Any<Guid>()).Returns(new DeveloperEvaluation.Domain.Entities.Company.Company("test", null, "1234567"));

            var product = Substitute.For<IProductRepository>();
            product.GetByIdAsync(Arg.Any<Guid>()).Returns(_fixture.GetValidProduct());

            var commandHandler = new UpdateSaleHandler(saleRepository, mapper, product, companyRepository, Substitute.For<ILogger<UpdateSaleHandler>>());

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
            var companyRepository = Substitute.For<ICompanyRepository>();
            companyRepository.GetByIdAsync(Guid.NewGuid()).Returns(new DeveloperEvaluation.Domain.Entities.Company.Company("test", null, "1234567"));
            var commandHandler = new UpdateSaleHandler(repository, Substitute.For<IMapper>(),
                Substitute.For<IProductRepository>(), companyRepository, Substitute.For<ILogger<UpdateSaleHandler>>());

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

            var companyRepository = Substitute.For<ICompanyRepository>();
            companyRepository.GetByIdAsync(Guid.NewGuid()).Returns(new DeveloperEvaluation.Domain.Entities.Company.Company("test", null, "1234567"));
            var commandHandler = new UpdateSaleHandler(saleRepository, Substitute.For<IMapper>(),
                Substitute.For<IProductRepository>(), companyRepository, Substitute.For<ILogger<UpdateSaleHandler>>());

            //act
            var exceptions = await Record.ExceptionAsync(() => commandHandler.Handle(_fixture.ValidUpdateSaleCommandMock(), CancellationToken.None));

            //assert
            Assert.NotNull(exceptions);
        }

        [Fact(DisplayName = nameof(Should_Be_Update_Sale_With_Not_Found_Product))]
        [Trait("Sale", nameof(UpdateSaleHandlerTests))]
        public async Task Should_Be_Update_Sale_With_Not_Found_Product()
        {
            //arrange
            var sale = _fixture.GetInvalidSale();
            var saleRepository = Substitute.For<ISaleRepository>();
            saleRepository.GetExistingSaleForUser(Arg.Any<Guid>(), CancellationToken.None).Returns(sale);

            var companyRepository = Substitute.For<ICompanyRepository>();
            companyRepository.GetByIdAsync(Guid.NewGuid()).Returns(new DeveloperEvaluation.Domain.Entities.Company.Company("test", null, "1234567"));

            var productRepository = Substitute.For<IProductRepository>();
            productRepository.GetByIdAsync(Arg.Any<Guid>()).ReturnsNull();

            var commandHandler = new UpdateSaleHandler(saleRepository, Substitute.For<IMapper>(), productRepository, companyRepository, Substitute.For<ILogger<UpdateSaleHandler>>());

            //act
            var exceptions = await Record.ExceptionAsync(() => commandHandler.Handle(_fixture.ValidUpdateSaleCommandMock(), CancellationToken.None));

            //assert
            Assert.NotNull(exceptions);
        }

        [Fact(DisplayName = nameof(Should_Be_Update_Sale_With_Empty_Items))]
        [Trait("Sale", nameof(UpdateSaleHandlerTests))]
        public async Task Should_Be_Update_Sale_With_Empty_Items()
        {
            //arrange
            var sale = _fixture.GetInvalidSaleWithoutItems();
            var saleRepository = Substitute.For<ISaleRepository>();
            saleRepository.GetExistingSaleForUser(Arg.Any<Guid>(), CancellationToken.None).Returns(sale);

            var companyRepository = Substitute.For<ICompanyRepository>();
            companyRepository.GetByIdAsync(Guid.NewGuid()).Returns(new DeveloperEvaluation.Domain.Entities.Company.Company("test", null, "1234567"));

            var productRepository = Substitute.For<IProductRepository>();
            productRepository.GetByIdAsync(Arg.Any<Guid>()).ReturnsNull();

            var commandHandler = new UpdateSaleHandler(saleRepository, Substitute.For<IMapper>(), productRepository, companyRepository, Substitute.For<ILogger<UpdateSaleHandler>>());

            //act
            var exceptions = await Record.ExceptionAsync(() => commandHandler.Handle(_fixture.ValidUpdateSaleCommandMock(), CancellationToken.None));

            //assert
            Assert.NotNull(exceptions);
        }
        #endregion
    }
}
