using Ambev.DeveloperEvaluation.Application.Sales.CreateSale;
using Ambev.DeveloperEvaluation.Domain.Entities.Sale;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Ambev.DeveloperEvaluation.Unit.Application.Product.Fixture;
using Ambev.DeveloperEvaluation.Unit.Application.Sale.Fixture;
using AutoMapper;
using NSubstitute;
using NSubstitute.ReturnsExtensions;
using Rebus.Compression;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Application.Sale
{
    [Collection(nameof(SaleTestsFixtureCollection))]
    public class CreateSaleHandlerTests
    {
        private readonly SaleTestsFixture _fixture;
        public CreateSaleHandlerTests(SaleTestsFixture fixture)
        {
            _fixture = fixture;
        }
        #region Success
        [Fact(DisplayName = nameof(Should_Be_Create_Sale_Product))]
        [Trait("Sale", nameof(CreateSaleHandlerTests))]
        public async Task Should_Be_Create_Sale_Product()
        {
            //arrange
            var saleRepository = Substitute.For<ISaleRepository>();
            saleRepository.AddAsync(Arg.Any<DeveloperEvaluation.Domain.Entities.Sale.Sale>())
                .Returns(_fixture.GetValidSale());
            saleRepository.GetExistingSaleForUser(Arg.Any<Guid>(), CancellationToken.None).ReturnsNull();

            var mapper = Substitute.For<IMapper>();
            mapper.Map<DeveloperEvaluation.Domain.Entities.Sale.Sale>(Arg.Any<CreateSaleCommand>()).Returns(_fixture.GetValidSale());
            
            var companyRepository = Substitute.For<ICompanyRepository>();
            companyRepository.GetByIdAsync(Arg.Any<Guid>()).Returns(new DeveloperEvaluation.Domain.Entities.Company.Company("test", null, "1234567"));

            var productRepository = Substitute.For<IProductRepository>();
            productRepository.GetByIdAsync(Arg.Any<Guid>()).Returns(_fixture.GetValidProduct());

            var commandHandler = new CreateSaleHandler(saleRepository, mapper, productRepository, companyRepository);

            //act
            var exceptions = await Record.ExceptionAsync(() => commandHandler.Handle(_fixture.ValidCreateSaleCommandMock(), CancellationToken.None));

            //assert
            Assert.Null(exceptions);
        }
        #endregion
        #region Error
        [Fact(DisplayName = nameof(Should_Be_Create_Sale_With_Existing_Open_Sale))]
        [Trait("Sale", nameof(CreateSaleHandlerTests))]
        public async Task Should_Be_Create_Sale_With_Existing_Open_Sale()
        {
            //arrange
            var sale = _fixture.GetValidSale();
            var saleRepository = Substitute.For<ISaleRepository>();
            saleRepository.GetExistingSaleForUser(Arg.Any<Guid>(), CancellationToken.None).Returns(sale);

            var companyRepository = Substitute.For<ICompanyRepository>();
            companyRepository.GetByIdAsync(Guid.NewGuid()).Returns(new DeveloperEvaluation.Domain.Entities.Company.Company("test", null, "1234567"));

            var commandHandler = new CreateSaleHandler(saleRepository, Substitute.For<IMapper>(), Substitute.For<IProductRepository>(), companyRepository);

            //act
            var exceptions = await Record.ExceptionAsync(() => commandHandler.Handle(_fixture.ValidCreateSaleCommandMock(), CancellationToken.None));

            //assert
            Assert.NotNull(exceptions);
        }

        [Fact(DisplayName = nameof(Should_Be_Create_Sale_With_Invalid_Sale))]
        [Trait("Sale", nameof(CreateSaleHandlerTests))]
        public async Task Should_Be_Create_Sale_With_Invalid_Sale()
        {
            //arrange
            var sale = _fixture.GetInvalidSale();
            var saleRepository = Substitute.For<ISaleRepository>();
            saleRepository.GetExistingSaleForUser(Arg.Any<Guid>(), CancellationToken.None).Returns(sale);

            var companyRepository = Substitute.For<ICompanyRepository>();
            companyRepository.GetByIdAsync(Guid.NewGuid()).Returns(new DeveloperEvaluation.Domain.Entities.Company.Company("test", null, "1234567"));

            var commandHandler = new CreateSaleHandler(saleRepository, Substitute.For<IMapper>(), Substitute.For<IProductRepository>(), companyRepository);

            //act
            var exceptions = await Record.ExceptionAsync(() => commandHandler.Handle(_fixture.ValidCreateSaleCommandMock(), CancellationToken.None));

            //assert
            Assert.NotNull(exceptions);
        }
        #endregion
    }
}
