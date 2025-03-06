using Ambev.DeveloperEvaluation.Domain.Entities.Product;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ambev.DeveloperEvaluation.ORM.Mapping
{
    public class DiscountConfiguration : IEntityTypeConfiguration<Discount>
    {
        public void Configure(EntityTypeBuilder<Discount> builder)
        {
            builder.ToTable("Discounts");
            builder.HasKey(d => d.Id);
            builder.Property(d => d.Id).HasColumnType("uuid").HasDefaultValueSql("gen_random_uuid()");

            builder.Property(c => c.IsDeleted).HasDefaultValue(false);

            builder.Property(d => d.ProductId).IsRequired().HasMaxLength(36);
            builder.HasOne(p => p.Product).WithOne()
                .HasForeignKey<Discount>(p => p.ProductId).IsRequired();

            builder.Property(d => d.Value).IsRequired();
            builder.Property(d => d.Quantity).IsRequired();

            builder.HasOne(d => d.Product).WithMany(p => p.Discounts)
                .HasForeignKey(d => d.ProductId).IsRequired();

            builder.HasIndex(d => new { d.ProductId, d.CompanyId, d.Quantity, d.StartDate, d.EndDate }).IsUnique();
        }
    }
}