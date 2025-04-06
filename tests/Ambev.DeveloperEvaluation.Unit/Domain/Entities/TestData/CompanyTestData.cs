using Ambev.DeveloperEvaluation.Domain.Entities.Company;
using System;

namespace Ambev.DeveloperEvaluation.Unit.Domain.Entities.TestData
{
    public static class CompanyTestData
    {
        public static Company GenerateValidCompany()
        {
            return new Company
            {
                Id = Guid.NewGuid(),
                Name = "Valid Company",
                Cnpj = "12345678000195",
                ParentCompanyId = null
            };
        }

        public static Company GenerateInvalidCompany()
        {
            return new Company
            {
                Id = Guid.NewGuid(),
                Name = "", // Invalid: Name cannot be empty
                Cnpj = "invalidcnpj", // Invalid: CNPJ format is incorrect
                ParentCompanyId = Guid.NewGuid()
            };
        }
    }
}
