namespace Ambev.DeveloperEvaluation.Unit.Domain.Entities.TestData
{
    public static class ProductTestData
    {
        public static DeveloperEvaluation.Domain.Entities.Product.Product GenerateValidProduct()
        {
            return new DeveloperEvaluation.Domain.Entities.Product.Product
            {
                Id = Guid.NewGuid(),
                Name = "Valid Product",
                Price = 100.00m,
                CategoryId = Guid.NewGuid()
            };
        }

        public static DeveloperEvaluation.Domain.Entities.Product.Product GenerateInvalidProduct()
        {
            return new DeveloperEvaluation.Domain.Entities.Product.Product
            {
                Id = Guid.NewGuid(),
                Name = "", // Invalid: Name cannot be empty
                Price = -100.00m, // Invalid: Price cannot be negative
                CategoryId = Guid.Empty // Invalid: CategoryId cannot be empty
            };
        }
    }
}
