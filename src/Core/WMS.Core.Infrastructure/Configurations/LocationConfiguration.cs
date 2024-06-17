using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using WMS.Core.Domain.Entities;

namespace WMS.Core.Infrastructure.Configurations;

internal sealed class LocationConfiguration : IEntityTypeConfiguration<Location>
{
    public void Configure(EntityTypeBuilder<Location> builder)
    {
        builder.HasKey(o => o.RowId);
        builder.Property(o => o.RowId).ValueGeneratedOnAdd();

        builder.Property(o => o.Code).HasMaxLength(50).IsRequired();
        builder.HasIndex(o => o.Code).IsUnique();

        builder.Property(o => o.Name).HasMaxLength(50).HasDefaultValue(string.Empty);

        builder.Property(o => o.PointX).HasMaxLength(20).IsRequired();
        builder.Property(o => o.PointY).HasMaxLength(20).IsRequired();
        builder.Property(o => o.PointZ).HasMaxLength(20).IsRequired();
        builder.Property(o => o.Capacity).IsRequired();

        builder.Property(o => o.IsActive).HasDefaultValue(true);
        builder.Property(o => o.IsDeleted).HasDefaultValue(false);
        builder.Property(o => o.CreatedAt).HasDefaultValueSql("GETDATE()");
        builder.Property(o => o.UpdatedAt).HasDefaultValueSql("GETDATE()");
    }
}