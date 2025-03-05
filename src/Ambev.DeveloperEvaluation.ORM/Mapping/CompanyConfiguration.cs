using Ambev.DeveloperEvaluation.Domain.Entities.Company;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ambev.DeveloperEvaluation.ORM.Mapping
{
    public class CompanyConfiguration : IEntityTypeConfiguration<Company>
    {
        public void Configure(EntityTypeBuilder<Company> builder)
        {
            builder.ToTable("Companies");
            builder.HasKey(s => s.Id);
            builder.Property(s => s.Id).HasColumnType("uuid").HasDefaultValueSql("gen_random_uuid()");

            builder.Property(c => c.IsDeleted).HasDefaultValue(false);

            builder.Property(s => s.Name).IsRequired().HasMaxLength(100);
            builder.Property(s => s.Cnpj).IsRequired().HasMaxLength(14);

            builder.Property(s => s.ParentCompanyId);
            builder.HasOne(s => s.ParentCompany).WithMany()
                .HasForeignKey(s => s.ParentCompanyId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}