using Ambev.DeveloperEvaluation.Application.Categories.DeleteCategory;
using Ambev.DeveloperEvaluation.Application.Sales.CreateSale;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Ambev.DeveloperEvaluation.Unit.Application.Category.Fixture;
using Microsoft.Extensions.Logging;
using NSubstitute;
using NSubstitute.ReturnsExtensions;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Application.Category
{
    [Collection(nameof(CategoryTestsFixtureCollection))]
    public class DeleteCategoryHandlerTests
    {
        private readonly CategoryTestsFixture _fixture;
        public DeleteCategoryHandlerTests(CategoryTestsFixture fixture)
        {
            _fixture = fixture;
        }
        #region Success
        [Fact(DisplayName = nameof(Should_Be_Delete_Successful_Category))]
        [Trait("Category", nameof(DeleteCategoryHandlerTests))]
        public async Task Should_Be_Delete_Successful_Category()
        {
            //arrange
            var categoryRepository = Substitute.For<ICategoryRepository>();
            categoryRepository.GetByIdAsync(Arg.Any<Guid>()).Returns(_fixture.GetValidCategory());
            categoryRepository.DeleteAsync(Arg.Any<DeveloperEvaluation.Domain.Entities.Product.Category>())
                .Returns(true);
            var commandHandler = new DeleteCategoryHandler(categoryRepository, Substitute.For<IProductRepository>(), Substitute.For<ILogger<DeleteCategoryHandler>>());

            //act
            var exceptions = await Record.ExceptionAsync(() => commandHandler.Handle(new DeleteCategoryCommand(Guid.NewGuid()), CancellationToken.None));

            //assert
            Assert.Null(exceptions);
        }
        #endregion

        #region Error
        [Fact(DisplayName = nameof(Should_Be_Delete_Not_Found_Category))]
        [Trait("Category", nameof(DeleteCategoryHandlerTests))]
        public async Task Should_Be_Delete_Not_Found_Category()
        {
            //arrange
            var categoryRepository = Substitute.For<ICategoryRepository>();
            categoryRepository.GetByIdAsync(Arg.Any<Guid>()).ReturnsNull();

            var commandHandler = new DeleteCategoryHandler(categoryRepository, Substitute.For<IProductRepository>(), Substitute.For<ILogger<DeleteCategoryHandler>>());

            //act
            var exceptions = await Record.ExceptionAsync(() => commandHandler.Handle(new DeleteCategoryCommand(new Guid()), CancellationToken.None));

            //assert
            Assert.NotNull(exceptions);
        }

        [Fact(DisplayName = nameof(Should_Be_Delete_Has_Products))]
        [Trait("Category", nameof(DeleteCategoryHandlerTests))]
        public async Task Should_Be_Delete_Has_Products()
        {
            //arrange
            var categoryRepository = Substitute.For<ICategoryRepository>();
            categoryRepository.GetByIdAsync(Arg.Any<Guid>()).Returns(_fixture.GetValidCategory());

            var productRepository = Substitute.For<IProductRepository>();
            productRepository.ExistsProductCategoryAsync(Arg.Any<Guid>(), Arg.Any<CancellationToken>()).Returns(true);

            var commandHandler = new DeleteCategoryHandler(categoryRepository, productRepository, Substitute.For<ILogger<DeleteCategoryHandler>>());

            //act
            var exceptions = await Record.ExceptionAsync(() => commandHandler.Handle(new DeleteCategoryCommand(new Guid()), CancellationToken.None));

            //assert
            Assert.NotNull(exceptions);
        }
        #endregion
    }
}
