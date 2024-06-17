using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using WMS.Core.Domain.Entities;

namespace WMS.Core.Infrastructure.Configurations;

internal sealed class ProductConfiguration : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        builder.HasKey(o => o.RowId);
        // Tự tăng
        builder.Property(o => o.RowId).ValueGeneratedOnAdd();

        builder.Property(o => o.Code).HasMaxLength(50).IsRequired();
        builder.HasIndex(o => o.Code).IsUnique();

        builder.Property(o => o.Name).HasMaxLength(50).HasDefaultValue(string.Empty);
        builder.Property(o => o.ImagePath).HasDefaultValue(string.Empty);

        builder.Property(o => o.ExpirationPeriodType).IsRequired();
        builder.Property(o => o.ExpirationPeriodValue).IsRequired();

        builder.Property(o => o.IsActive).HasDefaultValue(true);
        builder.Property(o => o.IsDeleted).HasDefaultValue(false);
        builder.Property(o => o.CreatedAt).HasDefaultValueSql("GETDATE()");
        builder.Property(o => o.UpdatedAt).HasDefaultValueSql("GETDATE()");
    }
}