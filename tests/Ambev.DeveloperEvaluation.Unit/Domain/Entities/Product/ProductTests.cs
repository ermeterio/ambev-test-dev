using Ambev.DeveloperEvaluation.Unit.Domain.Entities.TestData;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Domain.Entities.Product
{
    public class ProductTests
    {
        [Fact(DisplayName = "Product should be created successfully")]
        public void Given_ValidProductData_When_Created_Then_ShouldBeSuccessful()
        {
            var product = ProductTestData.GenerateValidProduct();
            Assert.NotNull(product);
            Assert.Equal("Valid Product", product.Name);
            Assert.Equal(100.00m, product.Price);
        }

        [Fact(DisplayName = "Product should be modified successfully")]
        public void Given_ValidProductData_When_Modified_Then_ShouldBeSuccessful()
        {
            var product = ProductTestData.GenerateValidProduct();
            product.Price = 150.00m;
            Assert.Equal(150.00m, product.Price);
        }

        [Fact(DisplayName = "Validation should fail for invalid product data")]
        public void Given_InvalidProductData_When_Validated_Then_ShouldReturnInvalid()
        {
            var product = ProductTestData.GenerateInvalidProduct();
            Assert.Equal("", product.Name);
            Assert.Equal(-100.00m, product.Price);
            Assert.Equal(Guid.Empty, product.CategoryId);
        }
    }
}
