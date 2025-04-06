using Ambev.DeveloperEvaluation.WebApi;
using Ambev.DeveloperEvaluation.WebApi.Features.Products;
using Microsoft.AspNetCore.Mvc.Testing;
using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Xunit;

namespace Ambev.DeveloperEvaluation.IntegrationTests
{
    public class ProductControllerTests : IClassFixture<WebApplicationFactory<Startup>>
    {
        private readonly HttpClient _client;

        public ProductControllerTests(WebApplicationFactory<Startup> factory)
        {
            _client = factory.CreateClient();
        }

        [Fact(DisplayName = "GET /api/product/{id} should return product by id")]
        public async Task GetProductById_ShouldReturnProduct()
        {
            // Arrange
            var productId = Guid.NewGuid(); // Use a valid product ID from your database or mock data

            // Act
            var response = await _client.GetAsync($"/api/product/{productId}");

            // Assert
            response.EnsureSuccessStatusCode();
            var product = await response.Content.ReadFromJsonAsync<GetProductResponse>();
            Assert.NotNull(product);
            Assert.Equal(productId, product.Id);
        }

        [Fact(DisplayName = "POST /api/product should create a new product")]
        public async Task CreateProduct_ShouldCreateNewProduct()
        {
            // Arrange
            var newProduct = new CreateProductRequest
            {
                Name = "New Product",
                Price = 100.00m,
                CategoryId = Guid.NewGuid(),
                Description = "A new product",
                ActualStock = 50,
                CompanyId = Guid.NewGuid(),
                MaxItemsForSale = 10
            };

            // Act
            var response = await _client.PostAsJsonAsync("/api/product", newProduct);

            // Assert
            response.EnsureSuccessStatusCode();
            var createdProduct = await response.Content.ReadFromJsonAsync<CreateProductResponse>();
            Assert.NotNull(createdProduct);
            Assert.Equal(newProduct.Name, createdProduct.Name);
        }

        [Fact(DisplayName = "PUT /api/product should update an existing product")]
        public async Task UpdateProduct_ShouldUpdateExistingProduct()
        {
            // Arrange
            var productId = Guid.NewGuid(); // Use a valid product ID from your database or mock data
            var updatedProduct = new UpdateProductRequest
            {
                Id = productId,
                Name = "Updated Product",
                Price = 150.00m,
                CategoryId = Guid.NewGuid(),
                Description = "An updated product",
                ActualStock = 100,
                CompanyId = Guid.NewGuid(),
                MaxItemsForSale = 20
            };

            // Act
            var response = await _client.PutAsJsonAsync("/api/product", updatedProduct);

            // Assert
            response.EnsureSuccessStatusCode();
            var updatedProductResponse = await response.Content.ReadFromJsonAsync<UpdateProductResponse>();
            Assert.NotNull(updatedProductResponse);
            Assert.Equal(updatedProduct.Name, updatedProductResponse.Name);
        }

        [Fact(DisplayName = "DELETE /api/product/{id} should delete an existing product")]
        public async Task DeleteProduct_ShouldDeleteExistingProduct()
        {
            // Arrange
            var productId = Guid.NewGuid(); // Use a valid product ID from your database or mock data

            // Act
            var response = await _client.DeleteAsync($"/api/product/{productId}");

            // Assert
            response.EnsureSuccessStatusCode();
            var deleteResponse = await response.Content.ReadFromJsonAsync<ApiResponse>();
            Assert.True(deleteResponse.Success);
        }

        [Fact(DisplayName = "POST /api/product/discount should create a new discount")]
        public async Task CreateDiscount_ShouldCreateNewDiscount()
        {
            // Arrange
            var newDiscount = new CreateDiscountRequest
            {
                ProductId = Guid.NewGuid(),
                Value = 10.00m,
                Quantity = 5,
                StartDate = DateTime.UtcNow,
                EndDate = DateTime.UtcNow.AddDays(10)
            };

            // Act
            var response = await _client.PostAsJsonAsync("/api/product/discount", newDiscount);

            // Assert
            response.EnsureSuccessStatusCode();
            var createdDiscount = await response.Content.ReadFromJsonAsync<CreateDiscountResponse>();
            Assert.NotNull(createdDiscount);
            Assert.Equal(newDiscount.Value, createdDiscount.Value);
        }

        [Fact(DisplayName = "DELETE /api/product/discount/{id} should delete an existing discount")]
        public async Task DeleteDiscount_ShouldDeleteExistingDiscount()
        {
            // Arrange
            var discountId = Guid.NewGuid(); // Use a valid discount ID from your database or mock data

            // Act
            var response = await _client.DeleteAsync($"/api/product/discount/{discountId}");

            // Assert
            response.EnsureSuccessStatusCode();
            var deleteResponse = await response.Content.ReadFromJsonAsync<ApiResponse>();
            Assert.True(deleteResponse.Success);
        }
    }
}
