using Ambev.DeveloperEvaluation.Domain.Entities.Product;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ambev.DeveloperEvaluation.ORM.Mapping
{
    public class RatingConfiguration : IEntityTypeConfiguration<Rating>
    {
        public void Configure(EntityTypeBuilder<Rating> builder)
        {
            builder.ToTable("Ratings");
            builder.HasKey(d => d.Id);
            builder.Property(d => d.Id).HasColumnType("uuid").HasDefaultValueSql("gen_random_uuid()");

            builder.Property(d => d.ProductId).IsRequired().HasMaxLength(36);
            builder.HasOne(p => p.Product).WithOne()
                .HasForeignKey<Rating>(p => p.ProductId).IsRequired();

            builder.Property(d => d.Rate).IsRequired();
            builder.Property(d => d.Comment).HasMaxLength(500);

            builder.Property(d => d.UserId).IsRequired().HasMaxLength(36);
            builder.HasOne(p => p.User)
                .WithOne()
                .HasForeignKey<Rating>(p => p.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasIndex(d => new { d.ProductId, d.Rate, d.UserId }).IsUnique();
        }
    }
}