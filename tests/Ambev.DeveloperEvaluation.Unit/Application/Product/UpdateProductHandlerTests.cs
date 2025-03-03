using Ambev.DeveloperEvaluation.Application.Products.CreateProduct;
using Ambev.DeveloperEvaluation.Application.Products.UpdateProduct;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Ambev.DeveloperEvaluation.Unit.Application.Product.Fixture;
using AutoMapper;
using NSubstitute;
using NSubstitute.ReturnsExtensions;
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
            var commandHandler = new UpdateProductHandler(productRepository, mapper);

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
            var commandHandler = new UpdateProductHandler(repository, Substitute.For<IMapper>());

            //act
            var exceptions = await Record.ExceptionAsync(() => commandHandler.Handle(_fixture.ValidUpdateProductCommand(), CancellationToken.None));

            //assert
            Assert.NotNull(exceptions);
        }
        
        [Fact(DisplayName = nameof(Should_Be_Unsuccessful_Update_Invalid_Product))]
        [Trait("Product", nameof(UpdateProductHandlerTests))]
        public async Task Should_Be_Unsuccessful_Update_Invalid_Product()
        {
            //arrange
            var repository = Substitute.For<IProductRepository>();
            var product = _fixture.GetInvalidProduct();
            repository.GetByNameAndCategoryAsync(Arg.Any<string>(), Arg.Any<Guid>(), Arg.Any<CancellationToken>()).Returns(product);
            var commandHandler = new UpdateProductHandler(repository, Substitute.For<IMapper>());

            //act
            var exceptions = await Record.ExceptionAsync(() => commandHandler.Handle(_fixture.ValidUpdateProductCommand(), CancellationToken.None));

            //assert
            Assert.NotNull(exceptions);
        }
        #endregion
    }
}