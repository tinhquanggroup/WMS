using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Attribute = WMS.Core.Domain.Entities.Attribute;


namespace WMS.Core.Infrastructure.Configurations
{
    internal sealed class AttributeConfiguration : IEntityTypeConfiguration<Attribute>
    {
        public void Configure(EntityTypeBuilder<Attribute> builder)
        {
            builder.HasKey(o => o.RowId);
            builder.Property(o => o.RowId).ValueGeneratedOnAdd();

            builder.Property(o => o.Name).IsRequired();
            builder.Property(o => o.Value).IsRequired();

            builder.Property(o => o.IsActive).HasDefaultValue(true);
            builder.Property(o => o.IsDeleted).HasDefaultValue(false);
            builder.Property(o => o.CreatedAt).HasDefaultValueSql("GETDATE()");
            builder.Property(o => o.UpdatedAt).HasDefaultValueSql("GETDATE()");
        }
    }
}
