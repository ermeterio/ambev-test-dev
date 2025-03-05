using Ambev.DeveloperEvaluation.Domain.Entities.Sale;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ambev.DeveloperEvaluation.ORM.Mapping
{
    public class SaleDiscountConfiguration : IEntityTypeConfiguration<SaleDiscount>
    {
        public void Configure(EntityTypeBuilder<SaleDiscount> builder)
        {
            builder.ToTable("SaleDiscounts");
            builder.HasKey(sd => sd.Id);
            builder.Property(sd => sd.Id).HasColumnType("uuid").HasDefaultValueSql("gen_random_uuid()");

            builder.Property(sd => sd.SaleId).IsRequired().HasMaxLength(36);
            builder.HasOne(sd => sd.Sale)
                .WithMany(s => s.Discounts)
                .HasForeignKey(sd => sd.SaleId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Property(s => s.DiscountId).IsRequired().HasMaxLength(36);
            builder.HasOne(s => s.Discount)
                .WithMany()
                .HasForeignKey(s => s.DiscountId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasIndex(s => new { s.DiscountId, s.SaleId }).IsUnique();
        }
    }
}