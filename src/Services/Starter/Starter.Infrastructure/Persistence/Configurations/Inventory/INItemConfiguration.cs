using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Starter.Infrastructure.Persistence.Configurations.Inventory
{
    public class INItemConfiguration : IEntityTypeConfiguration<INItem>
    {
        public void Configure(EntityTypeBuilder<INItem> builder)
        {
            builder.HasKey(c => c.Id);
            builder.Property(c => c.Id).HasConversion(
                    itemId => itemId.Value,
                    dbId => INItemID.Of(dbId));

            builder.Property(c => c.ItemCode).HasMaxLength(20).IsRequired();
            builder.HasIndex(c => c.ItemCode).IsUnique();

            builder.Property(c => c.ItemDesc).HasMaxLength(50).IsRequired();

            builder.Property(o => o.Price).HasPrecision(18, 4);
        }
    }
}
