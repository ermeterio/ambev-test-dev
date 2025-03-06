using Ambev.DeveloperEvaluation.Application.Categories.CreateCategory;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Ambev.DeveloperEvaluation.Unit.Application.Category.Fixture;
using AutoMapper;
using Castle.Core.Logging;
using Microsoft.Extensions.Logging;
using NSubstitute;
using NSubstitute.ReturnsExtensions;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Application.Category
{
    [Collection(nameof(CategoryTestsFixtureCollection))]
    public class CreateCategoryHandlerTests
    {
        private readonly CategoryTestsFixture _fixture;
        public CreateCategoryHandlerTests(CategoryTestsFixture fixture)
        {
            _fixture = fixture;
        }
        #region Success
        [Fact(DisplayName = nameof(Should_Be_Create_Successful_Category))]
        [Trait("Category", nameof(CreateCategoryHandlerTests))]
        public async Task Should_Be_Create_Successful_Category()
        {
            //arrange
            var categoryRepository = Substitute.For<ICategoryRepository>();
            categoryRepository.AddAsync(Arg.Any<DeveloperEvaluation.Domain.Entities.Product.Category>()).Returns(_fixture.GetValidCategory());

            var companyRepository = Substitute.For<ICompanyRepository>();
            var commandHandler = new CreateCategoryHandler(categoryRepository, companyRepository, Substitute.For<IMapper>(), Substitute.For<ILogger<CreateCategoryHandler>>());

            //act
            var exceptions = await Record.ExceptionAsync(() => commandHandler.Handle(_fixture.ValidCreateCategoryCommandMock(), CancellationToken.None));

            //assert
            Assert.Null(exceptions);
        }
        #endregion

        #region Error
        [Fact(DisplayName = nameof(Should_Be_Create_Existing_Category))]
        [Trait("Category", nameof(CreateCategoryHandlerTests))]
        public async Task Should_Be_Create_Existing_Category()
        {
            //arrange
            var categoryRepository = Substitute.For<ICategoryRepository>();
            categoryRepository.GetByNameAsync(Arg.Any<string>(), Arg.Any<Guid>()).Returns(_fixture.GetValidCategory());

            var companyRepository = Substitute.For<ICompanyRepository>();
            companyRepository.GetByIdAsync(Arg.Any<Guid>()).Returns(_fixture.GetValidCompany());

            var commandHandler = new CreateCategoryHandler(categoryRepository, companyRepository, Substitute.For<IMapper>(), Substitute.For<ILogger<CreateCategoryHandler>>());

            //act
            var exceptions = await Record.ExceptionAsync(() => commandHandler.Handle(_fixture.ValidCreateCategoryCommandMock(), CancellationToken.None));

            //assert
            Assert.NotNull(exceptions);
        }

        [Fact(DisplayName = nameof(Should_Be_Create_Existing_Category))]
        [Trait("Category", nameof(CreateCategoryHandlerTests))]
        public async Task Should_Be_Create_Not_Found_Company_Category()
        {
            //arrange
            var categoryRepository = Substitute.For<ICategoryRepository>();
            var companyRepository = Substitute.For<ICompanyRepository>();
            companyRepository.GetByIdAsync(Arg.Any<Guid>()).ReturnsNull();

            var commandHandler = new CreateCategoryHandler(categoryRepository, companyRepository, Substitute.For<IMapper>(), Substitute.For<ILogger<CreateCategoryHandler>>());

            //act
            var exceptions = await Record.ExceptionAsync(() => commandHandler.Handle(_fixture.ValidCreateCategoryCommandMock(), CancellationToken.None));

            //assert
            Assert.NotNull(exceptions);
        }

        [Fact(DisplayName = nameof(Should_Be_Create_Invalid_Category))]
        [Trait("Category", nameof(CreateCategoryHandlerTests))]
        public async Task Should_Be_Create_Invalid_Category()
        {
            //arrange
            var loggerMock = Substitute.For<ILogger<CreateCategoryHandler>>();

            var categoryRepository = Substitute.For<ICategoryRepository>();
            var companyRepository = Substitute.For<ICompanyRepository>();
            var commandHandler = new CreateCategoryHandler(categoryRepository, companyRepository, Substitute.For<IMapper>(), loggerMock);

            //act
            var exceptions = await Record.ExceptionAsync(() => commandHandler.Handle(_fixture.InvalidCreateCategoryCommandMock(), CancellationToken.None));

            //assert
            Assert.NotNull(exceptions);
        }
        #endregion
    }
}
