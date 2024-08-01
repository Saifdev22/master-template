using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Starter.Infrastructure.Data.Configurations
{
    public class CategoryConfiguration : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.HasKey(c => c.Id);
            //builder.Property(c => c.Id).HasConversion(
            //        customerId => customerId.Value,
            //        dbId => CustomerId.Of(dbId));

            builder.Property(c => c.CategoryCode)
                .HasMaxLength(15)
                .IsRequired();

            builder.Property(c => c.CategoryDesc).HasMaxLength(50);

            builder.HasIndex(c => c.CategoryCode).IsUnique();
        }
    }
}
