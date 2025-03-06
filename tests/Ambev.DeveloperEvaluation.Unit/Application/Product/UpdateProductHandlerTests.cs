using Ambev.DeveloperEvaluation.Application.Products.CreateProduct;
using Ambev.DeveloperEvaluation.Application.Products.UpdateProduct;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Ambev.DeveloperEvaluation.Unit.Application.Product.Fixture;
using AutoMapper;
using Microsoft.Extensions.Logging;
using NSubstitute;
using NSubstitute.ReturnsExtensions;
using Rebus.Bus;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Application.Product
{
    [Collection(nameof(ProductTestsFixtureCollection))]
    public class UpdateProductHandlerTests
    {
        private readonly ProductTestsFixture _fixture;
        public UpdateProductHandlerTests(ProductTestsFixture fixture)
        {
            _fixture = fixture;
        }
        #region Success
        [Fact(DisplayName = nameof(Should_Be_Update_Successful_Product))]
        [Trait("Product", nameof(UpdateProductHandlerTests))]
        public async Task Should_Be_Update_Successful_Product()
        {
            //arrange
            var product = _fixture.GetValidProduct();
            var productRepository = Substitute.For<IProductRepository>();
            productRepository.GetByIdAsync(Arg.Any<Guid>()).Returns(product);
            productRepository.UpdateAsync(Arg.Any<DeveloperEvaluation.Domain.Entities.Product.Product>(),
                    Arg.Any<CancellationToken>())
                .Returns(product);
            var mapper = Substitute.For<IMapper>();
            mapper.Map<DeveloperEvaluation.Domain.Entities.Product.Product>(Arg.Any<UpdateProductCommand>()).Returns(product);

            var companyRepository = Substitute.For<ICompanyRepository>();
            companyRepository.GetByIdAsync(Arg.Any<Guid>()).Returns(new DeveloperEvaluation.Domain.Entities.Company.Company("test", null, "1234567"));

            var categoryRepository = Substitute.For<ICategoryRepository>();
            categoryRepository.GetByIdAsync(Arg.Any<Guid>()).Returns(new DeveloperEvaluation.Domain.Entities.Product.Category { CompanyId = Guid.NewGuid(), Id = Guid.NewGuid(), Name = "test" });

            var commandHandler = new UpdateProductHandler(productRepository, mapper, companyRepository, categoryRepository, Substitute.For<IBus>(), Substitute.For<ILogger<UpdateProductHandler>>());

            //act
            var exceptions = await Record.ExceptionAsync(() => commandHandler.Handle(_fixture.ValidUpdateProductCommand(), CancellationToken.None));

            //assert
            Assert.Null(exceptions);
        }
        #endregion
        #region Error
        [Fact(DisplayName = nameof(Should_Be_Unsuccessful_Update_Not_Found_Product))]
        [Trait("Product", nameof(UpdateProductHandlerTests))]
        public async Task Should_Be_Unsuccessful_Update_Not_Found_Product()
        {
            //arrange
            var repository = Substitute.For<IProductRepository>();
            repository.GetByIdAsync(Arg.Any<Guid>()).ReturnsNull();

            var companyRepository = Substitute.For<ICompanyRepository>();
            companyRepository.GetByIdAsync(Arg.Any<Guid>()).Returns(new DeveloperEvaluation.Domain.Entities.Company.Company("test", null, "1234567"));

            var categoryRepository = Substitute.For<ICategoryRepository>();
            categoryRepository.GetByIdAsync(Arg.Any<Guid>()).Returns(new DeveloperEvaluation.Domain.Entities.Product.Category { CompanyId = Guid.NewGuid(), Id = Guid.NewGuid(), Name = "test" });

            var commandHandler = new UpdateProductHandler(repository, Substitute.For<IMapper>(), companyRepository, categoryRepository, Substitute.For<IBus>(), Substitute.For<ILogger<UpdateProductHandler>>());

            //act
            var exceptions = await Record.ExceptionAsync(() => commandHandler.Handle(_fixture.ValidUpdateProductCommand(), CancellationToken.None));

            //assert
            Assert.NotNull(exceptions);
        }

        [Fact(DisplayName = nameof(Should_Be_Unsuccessful_Update_Not_Found_CompanyId))]
        [Trait("Product", nameof(UpdateProductHandlerTests))]
        public async Task Should_Be_Unsuccessful_Update_Not_Found_CompanyId()
        {
            //arrange
            var repository = Substitute.For<IProductRepository>();
            var product = _fixture.GetInvalidProductWithNoCompanyId();
            repository.GetByNameAndCategoryAsync(Arg.Any<string>(), Arg.Any<Guid>(), Arg.Any<CancellationToken>()).Returns(product);
            var companyRepository = Substitute.For<ICompanyRepository>();
            companyRepository.GetByIdAsync(Guid.NewGuid()).Returns(new DeveloperEvaluation.Domain.Entities.Company.Company("test", null, "1234567"));

            var categoryRepository = Substitute.For<ICategoryRepository>();
            categoryRepository.GetByIdAsync(Arg.Any<Guid>()).Returns(new DeveloperEvaluation.Domain.Entities.Product.Category() { CompanyId = Guid.NewGuid(), Id = Guid.NewGuid(), Name = "test" });

            var commandHandler = new UpdateProductHandler(repository, Substitute.For<IMapper>(), companyRepository, categoryRepository, Substitute.For<IBus>(), Substitute.For<ILogger<UpdateProductHandler>>());

            //act
            var exceptions = await Record.ExceptionAsync(() => commandHandler.Handle(_fixture.ValidUpdateProductCommand(), CancellationToken.None));

            //assert
            Assert.NotNull(exceptions);
        }

        [Fact(DisplayName = nameof(Should_Be_Update_Invalid_Product_With_No_Name))]
        [Trait("Product", nameof(UpdateProductHandlerTests))]
        public async Task Should_Be_Update_Invalid_Product_With_No_Name()
        {
            //arrange
            var repository = Substitute.For<IProductRepository>();
            var product = _fixture.GetInvalidProductWithNoName();
            repository.GetByNameAndCategoryAsync(Arg.Any<string>(), Arg.Any<Guid>(), Arg.Any<CancellationToken>()).Returns(product);

            var companyRepository = Substitute.For<ICompanyRepository>();
            companyRepository.GetByIdAsync(Guid.NewGuid()).Returns(new DeveloperEvaluation.Domain.Entities.Company.Company("test", null, "1234567"));

            var categoryRepository = Substitute.For<ICategoryRepository>();
            categoryRepository.GetByIdAsync(Guid.NewGuid()).Returns(new DeveloperEvaluation.Domain.Entities.Product.Category() { CompanyId = Guid.NewGuid(), Id = Guid.NewGuid(), Name = "test" });

            var commandHandler = new UpdateProductHandler(repository, Substitute.For<IMapper>(), companyRepository, categoryRepository, Substitute.For<IBus>(), Substitute.For<ILogger<UpdateProductHandler>>());

            //act
            var exceptions = await Record.ExceptionAsync(() => commandHandler.Handle(_fixture.ValidUpdateProductCommand(), CancellationToken.None));

            //assert
            Assert.NotNull(exceptions);
        }

        [Fact(DisplayName = nameof(Should_Be_Update_Invalid_Product_With_No_Description))]
        [Trait("Product", nameof(UpdateProductHandlerTests))]
        public async Task Should_Be_Update_Invalid_Product_With_No_Description()
        {
            //arrange
            var repository = Substitute.For<IProductRepository>();
            var product = _fixture.GetInvalidProductWithNoDescription();
            repository.GetByNameAndCategoryAsync(Arg.Any<string>(), Arg.Any<Guid>(), Arg.Any<CancellationToken>()).Returns(product);

            var companyRepository = Substitute.For<ICompanyRepository>();
            companyRepository.GetByIdAsync(Guid.NewGuid()).Returns(new DeveloperEvaluation.Domain.Entities.Company.Company("test", null, "1234567"));

            var categoryRepository = Substitute.For<ICategoryRepository>();
            categoryRepository.GetByIdAsync(Guid.NewGuid()).Returns(new DeveloperEvaluation.Domain.Entities.Product.Category() { CompanyId = Guid.NewGuid(), Id = Guid.NewGuid(), Name = "test" });

            var commandHandler = new UpdateProductHandler(repository, Substitute.For<IMapper>(), companyRepository, categoryRepository, Substitute.For<IBus>(), Substitute.For<ILogger<UpdateProductHandler>>());

            //act
            var exceptions = await Record.ExceptionAsync(() => commandHandler.Handle(_fixture.ValidUpdateProductCommand(), CancellationToken.None));

            //assert
            Assert.NotNull(exceptions);
        }

        [Fact(DisplayName = nameof(Should_Be_Update_Invalid_Product_With_No_Name))]
        [Trait("Product", nameof(UpdateProductHandlerTests))]
        public async Task Should_Be_Update_Invalid_Product_With_No_Name()
        {
            //arrange
            var repository = Substitute.For<IProductRepository>();
            var product = _fixture.GetInvalidProductWithNoName();
            repository.GetByNameAndCategoryAsync(Arg.Any<string>(), Arg.Any<Guid>(), Arg.Any<CancellationToken>()).Returns(product);

            var companyRepository = Substitute.For<ICompanyRepository>();
            companyRepository.GetByIdAsync(Guid.NewGuid()).Returns(new DeveloperEvaluation.Domain.Entities.Company.Company("test", null, "1234567"));

            var categoryRepository = Substitute.For<ICategoryRepository>();
            categoryRepository.GetByIdAsync(Guid.NewGuid()).Returns(new DeveloperEvaluation.Domain.Entities.Product.Category() { CompanyId = Guid.NewGuid(), Id = Guid.NewGuid(), Name = "test" });

            var commandHandler = new UpdateProductHandler(repository, Substitute.For<IMapper>(), companyRepository, categoryRepository, Substitute.For<IBus>());

            //act
            var exceptions = await Record.ExceptionAsync(() => commandHandler.Handle(_fixture.ValidUpdateProductCommand(), CancellationToken.None));

            //assert
            Assert.NotNull(exceptions);
        }

        [Fact(DisplayName = nameof(Should_Be_Update_Invalid_Product_With_No_Description))]
        [Trait("Product", nameof(UpdateProductHandlerTests))]
        public async Task Should_Be_Update_Invalid_Product_With_No_Description()
        {
            //arrange
            var repository = Substitute.For<IProductRepository>();
            var product = _fixture.GetInvalidProductWithNoDescription();
            repository.GetByNameAndCategoryAsync(Arg.Any<string>(), Arg.Any<Guid>(), Arg.Any<CancellationToken>()).Returns(product);

            var companyRepository = Substitute.For<ICompanyRepository>();
            companyRepository.GetByIdAsync(Guid.NewGuid()).Returns(new DeveloperEvaluation.Domain.Entities.Company.Company("test", null, "1234567"));

            var categoryRepository = Substitute.For<ICategoryRepository>();
            categoryRepository.GetByIdAsync(Guid.NewGuid()).Returns(new DeveloperEvaluation.Domain.Entities.Product.Category() { CompanyId = Guid.NewGuid(), Id = Guid.NewGuid(), Name = "test" });

            var commandHandler = new UpdateProductHandler(repository, Substitute.For<IMapper>(), companyRepository, categoryRepository, Substitute.For<IBus>());

            //act
            var exceptions = await Record.ExceptionAsync(() => commandHandler.Handle(_fixture.ValidUpdateProductCommand(), CancellationToken.None));

            //assert
            Assert.NotNull(exceptions);
        }
        #endregion
    }
}