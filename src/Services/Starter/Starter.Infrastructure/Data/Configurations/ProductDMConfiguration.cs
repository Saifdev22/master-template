using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Starter.Infrastructure.Data.Configurations
{
    public class ProductDMConfiguration : IEntityTypeConfiguration<ProductDM>
    {
        public void Configure(EntityTypeBuilder<ProductDM> builder)
        {
            builder.HasKey(c => c.Id);
            //builder.Property(c => c.Id).HasConversion(
            //        customerId => customerId.Value,
            //        dbId => CustomerId.Of(dbId));

            builder.Property(c => c.Name).HasMaxLength(10).IsRequired();
            builder.Property(c => c.Quantity).HasMaxLength(5).IsRequired();

            //builder.HasIndex(c => c.Quantity).IsUnique();
        }
    }
}
