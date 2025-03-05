using Ambev.DeveloperEvaluation.Application.Products.CreateProduct;
using Ambev.DeveloperEvaluation.Application.Sales.CreateSale;
using Ambev.DeveloperEvaluation.Domain.Entities.Product;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Ambev.DeveloperEvaluation.ORM.Repositories;
using Ambev.DeveloperEvaluation.Unit.Application.Product.Fixture;
using AutoMapper;
using NSubstitute;
using Rebus.Bus;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Application.Product
{
    [Collection(nameof(ProductTestsFixtureCollection))]
    public class CreateProductHandlerTests
    {
        private readonly ProductTestsFixture _fixture;
        public CreateProductHandlerTests(ProductTestsFixture fixture)
        {
            _fixture = fixture;
        }
        #region Success
        [Fact(DisplayName = nameof(Should_Be_Create_Successful_Product))]
        [Trait("Product", nameof(CreateProductHandlerTests))]
        public async Task Should_Be_Create_Successful_Product()
        {
            //arrange
            var productRepository = Substitute.For<IProductRepository>();
            productRepository.AddAsync(Arg.Any<DeveloperEvaluation.Domain.Entities.Product.Product>(),
                    Arg.Any<CancellationToken>())
                .Returns(_fixture.GetValidProduct());

            var companyRepository = Substitute.For<ICompanyRepository>();
            companyRepository.GetByIdAsync(Arg.Any<Guid>()).Returns(new DeveloperEvaluation.Domain.Entities.Company.Company("test", null, "1234567"));

            var categoryRepository = Substitute.For<ICategoryRepository>();
            categoryRepository.GetByIdAsync(Arg.Any<Guid>()).Returns(new DeveloperEvaluation.Domain.Entities.Product.Category() { CompanyId = Guid.NewGuid(), Id = Guid.NewGuid(), Name = "test" });

            var commandHandler = new CreateProductHandler(productRepository, Substitute.For<IMapper>(), companyRepository, categoryRepository, Substitute.For<IBus>());

            //act
            var exceptions = await Record.ExceptionAsync(() => commandHandler.Handle(_fixture.ValidCreateProductCommandMock(), CancellationToken.None));

            //assert
            Assert.Null(exceptions);
        }
        #endregion

        #region Error
        [Fact(DisplayName = nameof(Should_Be_Create_Duplicate_Product))]
        [Trait("Product", nameof(CreateProductHandlerTests))]
        public async Task Should_Be_Create_Duplicate_Product()
        {
            //arrange
            var repository = Substitute.For<IProductRepository>();
            var product = _fixture.GetValidProduct();
            repository.GetByNameAndCategoryAsync(Arg.Any<string>(), Arg.Any<Guid>(), Arg.Any<CancellationToken>()).Returns(product);

            var companyRepository = Substitute.For<ICompanyRepository>();
            companyRepository.GetByIdAsync(Guid.NewGuid()).Returns(new DeveloperEvaluation.Domain.Entities.Company.Company("test", null, "1234567"));

            var categoryRepository = Substitute.For<ICategoryRepository>();
            categoryRepository.GetByIdAsync(Guid.NewGuid()).Returns(new DeveloperEvaluation.Domain.Entities.Product.Category(){CompanyId = Guid.NewGuid(), Id = Guid.NewGuid(), Name = "test"});

            var commandHandler = new CreateProductHandler(repository, Substitute.For<IMapper>(), companyRepository, categoryRepository, Substitute.For<IBus>());

            //act
            var exceptions = await Record.ExceptionAsync(() => commandHandler.Handle(_fixture.ValidCreateProductCommandMock(), CancellationToken.None));

            //assert
            Assert.NotNull(exceptions);
        }

        [Fact(DisplayName = nameof(Should_Be_Create_Invalid_Product))]
        [Trait("Product", nameof(CreateProductHandlerTests))]
        public async Task Should_Be_Create_Invalid_Product()
        {
            //arrange
            var repository = Substitute.For<IProductRepository>();
            var product = _fixture.GetInvalidProduct();
            repository.GetByNameAndCategoryAsync(Arg.Any<string>(), Arg.Any<Guid>(), Arg.Any<CancellationToken>()).Returns(product);

            var companyRepository = Substitute.For<ICompanyRepository>();
            companyRepository.GetByIdAsync(Guid.NewGuid()).Returns(new DeveloperEvaluation.Domain.Entities.Company.Company("test", null, "1234567"));

            var categoryRepository = Substitute.For<ICategoryRepository>();
            categoryRepository.GetByIdAsync(Guid.NewGuid()).Returns(new DeveloperEvaluation.Domain.Entities.Product.Category(){CompanyId = Guid.NewGuid(), Id = Guid.NewGuid(), Name = "test"});

            var commandHandler = new CreateProductHandler(repository, Substitute.For<IMapper>(), companyRepository, categoryRepository, Substitute.For<IBus>());

            //act
            var exceptions = await Record.ExceptionAsync(() => commandHandler.Handle(_fixture.ValidCreateProductCommandMock(), CancellationToken.None));

            //assert
            Assert.NotNull(exceptions);
        }
        #endregion
    }
}
