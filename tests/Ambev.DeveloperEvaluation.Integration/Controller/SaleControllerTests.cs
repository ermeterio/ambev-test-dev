using Ambev.DeveloperEvaluation.WebApi;
using Ambev.DeveloperEvaluation.WebApi.Features.Sales;
using Microsoft.AspNetCore.Mvc.Testing;
using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Xunit;

namespace Ambev.DeveloperEvaluation.IntegrationTests
{
    public class SaleControllerTests : IClassFixture<WebApplicationFactory<Startup>>
    {
        private readonly HttpClient _client;

        public SaleControllerTests(WebApplicationFactory<Startup> factory)
        {
            _client = factory.CreateClient();
        }

        [Fact(DisplayName = "GET /api/sale/{id} should return sale by id")]
        public async Task GetSaleById_ShouldReturnSale()
        {
            // Arrange
            var saleId = Guid.NewGuid(); // Use a valid sale ID from your database or mock data

            // Act
            var response = await _client.GetAsync($"/api/sale/{saleId}");

            // Assert
            response.EnsureSuccessStatusCode();
            var sale = await response.Content.ReadFromJsonAsync<GetSaleResponse>();
            Assert.NotNull(sale);
            Assert.Equal(saleId, sale.Id);
        }

        [Fact(DisplayName = "POST /api/sale should create a new sale")]
        public async Task CreateSale_ShouldCreateNewSale()
        {
            // Arrange
            var newSale = new CreateSaleRequest
            {
                CompanyId = Guid.NewGuid(),
                UserId = Guid.NewGuid(),
                Items = new List<SaleItem>
                {
                    new SaleItem
                    {
                        ProductId = Guid.NewGuid(),
                        Quantity = 5,
                        UnitPrice = 20.00m,
                        Discount = 0.10m,
                        TotalAmount = 90.00m
                    }
                },
                Status = SaleStatus.Processing
            };

            // Act
            var response = await _client.PostAsJsonAsync("/api/sale", newSale);

            // Assert
            response.EnsureSuccessStatusCode();
            var createdSale = await response.Content.ReadFromJsonAsync<CreateSaleResponse>();
            Assert.NotNull(createdSale);
            Assert.Equal(newSale.CompanyId, createdSale.CompanyId);
        }

        [Fact(DisplayName = "PUT /api/sale should update an existing sale")]
        public async Task UpdateSale_ShouldUpdateExistingSale()
        {
            // Arrange
            var saleId = Guid.NewGuid(); // Use a valid sale ID from your database or mock data
            var updatedSale = new UpdateSaleRequest
            {
                Id = saleId,
                CompanyId = Guid.NewGuid(),
                UserId = Guid.NewGuid(),
                Items = new List<SaleItem>
                {
                    new SaleItem
                    {
                        ProductId = Guid.NewGuid(),
                        Quantity = 10,
                        UnitPrice = 15.00m,
                        Discount = 0.20m,
                        TotalAmount = 120.00m
                    }
                },
                Status = SaleStatus.Finished
            };

            // Act
            var response = await _client.PutAsJsonAsync("/api/sale", updatedSale);

            // Assert
            response.EnsureSuccessStatusCode();
            var updatedSaleResponse = await response.Content.ReadFromJsonAsync<UpdateSaleResponse>();
            Assert.NotNull(updatedSaleResponse);
            Assert.Equal(updatedSale.CompanyId, updatedSaleResponse.CompanyId);
        }

        [Fact(DisplayName = "DELETE /api/sale/{id} should delete an existing sale")]
        public async Task DeleteSale_ShouldDeleteExistingSale()
        {
            // Arrange
            var saleId = Guid.NewGuid(); // Use a valid sale ID from your database or mock data

            // Act
            var response = await _client.DeleteAsync($"/api/sale/{saleId}");

            // Assert
            response.EnsureSuccessStatusCode();
            var deleteResponse = await response.Content.ReadFromJsonAsync<ApiResponse>();
            Assert.True(deleteResponse.Success);
        }
    }
}
