using Ambev.DeveloperEvaluation.Application.Companies.UpdateCompany;
using Ambev.DeveloperEvaluation.Application.Products.CreateDiscount;
using Ambev.DeveloperEvaluation.Domain.Entities.Product;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Ambev.DeveloperEvaluation.Unit.Application.Product.Fixture;
using AutoMapper;
using Microsoft.Extensions.Logging;
using NSubstitute;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Application.Product
{
    [Collection(nameof(ProductTestsFixtureCollection))]
    public class CreateDiscountHandlerTests
    {
        private readonly ProductTestsFixture _fixture;
        public CreateDiscountHandlerTests(ProductTestsFixture fixture)
        {
            _fixture = fixture;
        }
        #region Success
        [Fact(DisplayName = nameof(Should_Be_Create_Successful_Discount))]
        [Trait("Discount", nameof(CreateDiscountHandlerTests))]
        public async Task Should_Be_Create_Successful_Discount()
        {
            //arrange
            var discountRepository = Substitute.For<IDiscountRepository>();
            discountRepository.AddAsync(Arg.Any<DeveloperEvaluation.Domain.Entities.Product.Discount>(),
                    Arg.Any<CancellationToken>())
                .Returns(_fixture.GetValidDiscount());

            var productRepository = Substitute.For<IProductRepository>();
            productRepository.GetByIdAsync(Arg.Any<Guid>()).Returns(_fixture.GetValidProduct());

            var commandHandler = new CreateDiscountHandler(productRepository, discountRepository, Substitute.For<IMapper>(), Substitute.For<ILogger<CreateDiscountHandler>>());

            //act
            var exceptions = await Record.ExceptionAsync(() => commandHandler.Handle(_fixture.ValidCreateDiscountCommandMock(), CancellationToken.None));

            //assert
            Assert.Null(exceptions);
        }
        #endregion

        #region Error
        [Fact(DisplayName = nameof(Should_Be_Create_Duplicate_Discount))]
        [Trait("Discount", nameof(CreateDiscountHandlerTests))]
        public async Task Should_Be_Create_Duplicate_Discount()
        {
            //arrange
            var repository = Substitute.For<IDiscountRepository>();
            repository.GetIdenticalDiscount(Arg.Any<Discount>(), Arg.Any<CancellationToken>())
                .Returns(_fixture.GetValidDiscount());

            var productRepository = Substitute.For<IProductRepository>();
            productRepository.GetByIdAsync(Arg.Any<Guid>()).Returns(_fixture.GetValidProduct());

            var companyRepository = Substitute.For<ICompanyRepository>();
            companyRepository.GetByIdAsync(Guid.NewGuid()).Returns(new DeveloperEvaluation.Domain.Entities.Company.Company("test", null, "1234567"));

            var commandHandler = new CreateDiscountHandler(productRepository, repository, Substitute.For<IMapper>(), Substitute.For<ILogger<CreateDiscountHandler>>());

            //act
            var exceptions = await Record.ExceptionAsync(() => commandHandler.Handle(_fixture.ValidCreateDiscountCommandMock(), CancellationToken.None));

            //assert
            Assert.NotNull(exceptions);
        }
        #endregion
    }
}