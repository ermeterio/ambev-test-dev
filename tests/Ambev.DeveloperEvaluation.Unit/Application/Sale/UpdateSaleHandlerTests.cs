using Ambev.DeveloperEvaluation.Application.Sales.CreateSale;
using Ambev.DeveloperEvaluation.Unit.Application.Sale.Fixture;
using NSubstitute;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ambev.DeveloperEvaluation.Application.Sales.UpdateSale;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Application.Sale
{
    public class UpdateSaleHandlerTests
    {
        private readonly SaleTestsFixture _fixture;
        public UpdateSaleHandlerTests(SaleTestsFixture fixture)
        {
            _fixture = fixture;
        }

        [Fact(DisplayName = nameof(Should_Be_Update_Sale_Product))]
        [Trait("Sale", nameof(UpdateSaleHandlerTests))]
        public async Task Should_Be_Update_Sale_Product()
        {
            //arrange
            var commandHandler = Substitute.For<UpdateSaleHandler>();

            //act
            var exceptions = await Record.ExceptionAsync(() => commandHandler.Handle(_fixture.ValidUpdateSaleCommandMock(), CancellationToken.None));

            //assert
            Assert.Null(exceptions);
        }
    }
}
