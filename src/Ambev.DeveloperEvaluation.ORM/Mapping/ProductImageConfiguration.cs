using Ambev.DeveloperEvaluation.Domain.Entities.Product;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ambev.DeveloperEvaluation.ORM.Mapping
{
    public class ProductImageConfiguration : IEntityTypeConfiguration<ProductImage>
    {
        public void Configure(EntityTypeBuilder<ProductImage> builder)
        {
            builder.ToTable("ProductImages");
            builder.HasKey(x => x.Id);
            builder.Property(u => u.Id).HasColumnType("uuid").HasDefaultValueSql("gen_random_uuid()");
            
            builder.Property(x => x.ProductId).IsRequired();
            builder.Property(x => x.Image).IsRequired();
            builder.Property(x => x.Order).IsRequired();
            builder.Property(x => x.IsMain).IsRequired().HasDefaultValue(false);
        }
    }
}