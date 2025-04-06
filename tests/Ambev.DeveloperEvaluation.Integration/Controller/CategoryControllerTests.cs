using Ambev.DeveloperEvaluation.WebApi;
using Ambev.DeveloperEvaluation.WebApi.Features.Categories;
using Microsoft.AspNetCore.Mvc.Testing;
using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Xunit;

namespace Ambev.DeveloperEvaluation.IntegrationTests
{
    public class CategoryControllerTests : IClassFixture<WebApplicationFactory<Startup>>
    {
        private readonly HttpClient _client;

        public CategoryControllerTests(WebApplicationFactory<Startup> factory)
        {
            _client = factory.CreateClient();
        }

        [Fact(DisplayName = "GET /api/category/{id} should return category by id")]
        public async Task GetCategoryById_ShouldReturnCategory()
        {
            // Arrange
            var categoryId = Guid.NewGuid(); // Use a valid category ID from your database or mock data

            // Act
            var response = await _client.GetAsync($"/api/category/{categoryId}");

            // Assert
            response.EnsureSuccessStatusCode();
            var category = await response.Content.ReadFromJsonAsync<GetCategoryResponse>();
            Assert.NotNull(category);
            Assert.Equal(categoryId, category.Id);
        }

        [Fact(DisplayName = "POST /api/category should create a new category")]
        public async Task CreateCategory_ShouldCreateNewCategory()
        {
            // Arrange
            var newCategory = new CreateCategoryRequest
            {
                Name = "New Category",
                CompanyId = Guid.NewGuid()
            };

            // Act
            var response = await _client.PostAsJsonAsync("/api/category", newCategory);

            // Assert
            response.EnsureSuccessStatusCode();
            var createdCategory = await response.Content.ReadFromJsonAsync<CreateCategoryResponse>();
            Assert.NotNull(createdCategory);
            Assert.Equal(newCategory.Name, createdCategory.Name);
        }

        [Fact(DisplayName = "DELETE /api/category/{id} should delete an existing category")]
        public async Task DeleteCategory_ShouldDeleteExistingCategory()
        {
            // Arrange
            var categoryId = Guid.NewGuid(); // Use a valid category ID from your database or mock data

            // Act
            var response = await _client.DeleteAsync($"/api/category/{categoryId}");

            // Assert
            response.EnsureSuccessStatusCode();
            var deleteResponse = await response.Content.ReadFromJsonAsync<ApiResponse>();
            Assert.True(deleteResponse.Success);
        }
    }
}
