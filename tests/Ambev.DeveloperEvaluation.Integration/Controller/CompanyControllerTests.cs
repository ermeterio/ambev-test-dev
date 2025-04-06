using Ambev.DeveloperEvaluation.WebApi;
using Ambev.DeveloperEvaluation.WebApi.Features.Companies;
using Microsoft.AspNetCore.Mvc.Testing;
using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Xunit;

namespace Ambev.DeveloperEvaluation.IntegrationTests
{
    public class CompanyControllerTests : IClassFixture<WebApplicationFactory<Startup>>
    {
        private readonly HttpClient _client;

        public CompanyControllerTests(WebApplicationFactory<Startup> factory)
        {
            _client = factory.CreateClient();
        }

        [Fact(DisplayName = "GET /api/company/{id} should return company by id")]
        public async Task GetCompanyById_ShouldReturnCompany()
        {
            // Arrange
            var companyId = Guid.NewGuid(); // Use a valid company ID from your database or mock data

            // Act
            var response = await _client.GetAsync($"/api/company/{companyId}");

            // Assert
            response.EnsureSuccessStatusCode();
            var company = await response.Content.ReadFromJsonAsync<GetCompanyResponse>();
            Assert.NotNull(company);
            Assert.Equal(companyId, company.Id);
        }

        [Fact(DisplayName = "POST /api/company should create a new company")]
        public async Task CreateCompany_ShouldCreateNewCompany()
        {
            // Arrange
            var newCompany = new CreateCompanyRequest
            {
                Name = "New Company",
                Cnpj = "12345678000195",
                ParentCompanyId = null
            };

            // Act
            var response = await _client.PostAsJsonAsync("/api/company", newCompany);

            // Assert
            response.EnsureSuccessStatusCode();
            var createdCompany = await response.Content.ReadFromJsonAsync<CreateCompanyResponse>();
            Assert.NotNull(createdCompany);
            Assert.Equal(newCompany.Name, createdCompany.Name);
        }

        [Fact(DisplayName = "PUT /api/company should update an existing company")]
        public async Task UpdateCompany_ShouldUpdateExistingCompany()
        {
            // Arrange
            var companyId = Guid.NewGuid(); // Use a valid company ID from your database or mock data
            var updatedCompany = new UpdateCompanyRequest
            {
                Id = companyId,
                Name = "Updated Company",
                Cnpj = "98765432000195",
                ParentCompanyId = null
            };

            // Act
            var response = await _client.PutAsJsonAsync("/api/company", updatedCompany);

            // Assert
            response.EnsureSuccessStatusCode();
            var updatedCompanyResponse = await response.Content.ReadFromJsonAsync<UpdateCompanyResponse>();
            Assert.NotNull(updatedCompanyResponse);
            Assert.Equal(updatedCompany.Name, updatedCompanyResponse.Name);
        }

        [Fact(DisplayName = "DELETE /api/company/{id} should delete an existing company")]
        public async Task DeleteCompany_ShouldDeleteExistingCompany()
        {
            // Arrange
            var companyId = Guid.NewGuid(); // Use a valid company ID from your database or mock data

            // Act
            var response = await _client.DeleteAsync($"/api/company/{companyId}");

            // Assert
            response.EnsureSuccessStatusCode();
            var deleteResponse = await response.Content.ReadFromJsonAsync<ApiResponse>();
            Assert.True(deleteResponse.Success);
        }
    }
}

