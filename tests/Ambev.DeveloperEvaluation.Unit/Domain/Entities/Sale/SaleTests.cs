using Ambev.DeveloperEvaluation.Domain.Entities.Company;
using Ambev.DeveloperEvaluation.Domain.Entities.Sale;
using Ambev.DeveloperEvaluation.Domain.Enums;
using Ambev.DeveloperEvaluation.Unit.Domain.Entities.TestData;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Domain.Entities.Sale
{
    public class SaleTests
    {
        [Fact(DisplayName = "Sale should be created successfully")]
        public void Given_ValidSaleData_When_Created_Then_ShouldBeSuccessful()
        {
            var sale = SalesTestData.GenerateValidSale();
            Assert.NotNull(sale);
            Assert.NotNull(sale.Company);
            Assert.NotNull(sale.User);
            Assert.NotNull(sale.Items);
            Assert.NotEmpty(sale.Items);
            Assert.Equal(SaleStatus.Processing, sale.Status);
        }

        [Fact(DisplayName = "Sale should be modified successfully")]
        public void Given_ValidSaleData_When_Modified_Then_ShouldBeSuccessful()
        {
            var sale = SalesTestData.GenerateValidSale();
            var updatedSale = new DeveloperEvaluation.Domain.Entities.Sale.Sale
            {
                Company = new Company { Id = Guid.NewGuid(), Name = "Updated Company", Cnpj = "98765432000195" },
                UserId = Guid.NewGuid(),
                Discounts = new List<SaleDiscount>(),
                Items = new List<SaleItem>
                {
                    new SaleItem
                    {
                        Id = Guid.NewGuid(),
                        ProductId = Guid.NewGuid(),
                        Quantity = 10
                    }
                },
                Status = SaleStatus.Finished
            };
            sale.Update(updatedSale);
            Assert.Equal("Valid Company", sale.Company.Name);
            Assert.Equal(SaleStatus.Finished, sale.Status);
        }

        [Fact(DisplayName = "Sale should be cancelled successfully")]
        public void Given_ValidSaleData_When_Cancelled_Then_ShouldBeCancelled()
        {
            var sale = SalesTestData.GenerateValidSale();
            sale.Cancel();
            Assert.Equal(SaleStatus.Canceled, sale.Status);
        }
        
        [Fact(DisplayName = "Validation should fail for invalid sale data")]
        public void Given_InvalidSaleData_When_Validated_Then_ShouldReturnInvalid()
        {
            var sale = SalesTestData.GenerateInvalidSale();
            Assert.False(sale.IsValid());
        }
    }
}
