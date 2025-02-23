using Ambev.DeveloperEvaluation.Domain.Entities.Sale;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ambev.DeveloperEvaluation.ORM.Mapping
{
    public class SaleConfiguration : IEntityTypeConfiguration<Sale>
    {
        public void Configure(EntityTypeBuilder<Sale> builder)
        {
            builder.ToTable("Sales");
            builder.HasKey(s => s.Id);
            builder.Property(s => s.Id).HasColumnType("uuid").HasDefaultValueSql("gen_random_uuid()");

            builder.Property(s => s.UserId).IsRequired().HasMaxLength(36);
            builder.HasOne(s => s.User).WithMany()
                .HasForeignKey(s => s.UserId).IsRequired();

            builder.Property(s => s.Status).IsRequired();
            builder.HasMany(s => s.Discounts).WithOne().HasForeignKey(s => s.SaleId);
            builder.HasMany(s => s.Products).WithOne().HasForeignKey(s => s.SaleId);

            builder.Property(s => s.CompanyId).IsRequired().HasMaxLength(36);
            builder.HasOne(s => s.Company)
                .WithMany()
                .HasForeignKey(s => s.CompanyId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}