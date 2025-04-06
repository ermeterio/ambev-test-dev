using Ambev.DeveloperEvaluation.Domain.Entities.Company;
using Ambev.DeveloperEvaluation.Unit.Domain.Entities.TestData;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Domain.Entities
{
    public class CompanyTests
    {
        [Fact(DisplayName = "Company should be created successfully")]
        public void Given_ValidCompanyData_When_Created_Then_ShouldBeSuccessful()
        {
            var company = CompanyTestData.GenerateValidCompany();
            Assert.NotNull(company);
            Assert.Equal("Valid Company", company.Name);
            Assert.Equal("12345678000195", company.Cnpj);
            Assert.Null(company.ParentCompanyId);
        }

        [Fact(DisplayName = "Company should be updated successfully")]
        public void Given_ValidCompanyData_When_Updated_Then_ShouldBeSuccessful()
        {
            var company = CompanyTestData.GenerateValidCompany();
            var updatedCompany = new Company("Updated Company", null, "98765432000195");
            company.Update(updatedCompany);
            Assert.Equal("Updated Company", company.Name);
            Assert.Equal("98765432000195", company.Cnpj);
        }

        [Fact(DisplayName = "IsBranch should return true if ParentCompanyId is not null")]
        public void Given_CompanyWithParentCompanyId_When_CheckedIsBranch_Then_ShouldReturnTrue()
        {
            var company = CompanyTestData.GenerateValidCompany();
            company.ParentCompanyId = Guid.NewGuid();
            Assert.True(company.IsBranch);
        }

        [Fact(DisplayName = "IsBranch should return false if ParentCompanyId is null")]
        public void Given_CompanyWithoutParentCompanyId_When_CheckedIsBranch_Then_ShouldReturnFalse()
        {
            var company = CompanyTestData.GenerateValidCompany();
            company.ParentCompanyId = null;
            Assert.False(company.IsBranch);
        }

        [Fact(DisplayName = "Company should have valid CNPJ format")]
        public void Given_CompanyWithInvalidCnpj_When_Checked_Then_ShouldReturnInvalid()
        {
            var company = CompanyTestData.GenerateInvalidCompany();
            Assert.NotEqual("12345678000195", company.Cnpj);
        }
    }
}
