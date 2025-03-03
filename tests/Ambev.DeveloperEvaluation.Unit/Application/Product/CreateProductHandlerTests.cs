﻿using Ambev.DeveloperEvaluation.Application.Products.CreateProduct;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Ambev.DeveloperEvaluation.Unit.Application.Product.Fixture;
using AutoMapper;
using NSubstitute;
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
            var commandHandler = new CreateProductHandler(productRepository, Substitute.For<IMapper>());

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
            var commandHandler = new CreateProductHandler(repository, Substitute.For<IMapper>());

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
            var commandHandler = new CreateProductHandler(repository, Substitute.For<IMapper>());

            //act
            var exceptions = await Record.ExceptionAsync(() => commandHandler.Handle(_fixture.ValidCreateProductCommandMock(), CancellationToken.None));

            //assert
            Assert.NotNull(exceptions);
        }
        #endregion
    }
}
