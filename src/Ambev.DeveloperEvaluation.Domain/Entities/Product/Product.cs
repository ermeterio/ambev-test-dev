using Ambev.DeveloperEvaluation.Domain.Common;
using Ambev.DeveloperEvaluation.Domain.Events.Interface;

namespace Ambev.DeveloperEvaluation.Domain.Entities.Product
{
    public class Product : BaseEntity, IEntityWithEvents
    {
        public string Name { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public Guid CategoryId { get; set; }
        public Category? Category { get; set; }
        public ProductImage? Image { get; set; } 
        public string Description { get; set; } = string.Empty;
        public int ActualStock { get; set; }
        public Company.Company? Company { get; set; }
        public IEnumerable<Discount>? Discounts { get; set; }
        public int MaxItemsForSale { get; set; }

        public void Update(Product product)
        {
            if (Name != product.Name)
                Name = product.Name;
            if (Price != product.Price)
                Price = product.Price;
            if (CategoryId != product.CategoryId)
                CategoryId = product.CategoryId;
            if (Description != product.Description)
                Description = product.Description;
            if (ActualStock != product.ActualStock)
                ActualStock = product.ActualStock;
            if (CompanyId != product.CompanyId)
                CompanyId = product.CompanyId;
            if (MaxItemsForSale != product.MaxItemsForSale)
                MaxItemsForSale = product.MaxItemsForSale;
        }
    }
}