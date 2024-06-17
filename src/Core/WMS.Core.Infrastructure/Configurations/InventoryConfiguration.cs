﻿using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using WMS.Core.Domain.Entities;

namespace WMS.Core.Infrastructure.Configurations;

internal sealed class InventoryConfiguration : IEntityTypeConfiguration<Inventory>
{
    public void Configure(EntityTypeBuilder<Inventory> builder)
    {
        builder.HasKey(o => o.RowId);
        builder.Property(o => o.RowId).ValueGeneratedOnAdd();

        builder.Property(o => o.Quantity).IsRequired();

        builder.Property(o => o.IsActive).HasDefaultValue(true);
        builder.Property(o => o.IsDeleted).HasDefaultValue(false);
        builder.Property(o => o.CreatedAt).HasDefaultValueSql("GETDATE()");
        builder.Property(o => o.UpdatedAt).HasDefaultValueSql("GETDATE()");
    }
}