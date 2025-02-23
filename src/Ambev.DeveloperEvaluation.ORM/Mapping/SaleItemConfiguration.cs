using Ambev.DeveloperEvaluation.Domain.Entities.Sale;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ambev.DeveloperEvaluation.ORM.Mapping
{
    public class SaleItemConfiguration : IEntityTypeConfiguration<SaleItem>
    {
        public void Configure(EntityTypeBuilder<SaleItem> builder)
        {
            builder.ToTable("SaleItems");
            builder.HasKey(sd => sd.Id);
            builder.Property(sd => sd.Id).HasColumnType("uuid").HasDefaultValueSql("gen_random_uuid()");

            builder.Property(sd => sd.Quantity).IsRequired();
            builder.Property(sd => sd.SaleId).IsRequired().HasMaxLength(36);

            builder.HasOne(s => s.Sale)
                .WithMany(s => s.Products)
                .HasForeignKey(s => s.SaleId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Property(sd => sd.ProductId).IsRequired().HasMaxLength(36);
            builder.HasOne(s => s.Product)
                .WithMany()
                .HasForeignKey(s => s.ProductId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasIndex(s => new { s.ProductId, s.SaleId }).IsUnique();
        }
    }
}