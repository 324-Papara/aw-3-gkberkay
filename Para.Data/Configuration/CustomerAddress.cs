using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Para.Data.Domain;

namespace Para.Data.Configuration
{
    public class CustomerAddressConfiguration : IEntityTypeConfiguration<CustomerAddress>
    {
        public void Configure(EntityTypeBuilder<CustomerAddress> builder)
        {
            builder.Property(e => e.InsertUser).IsRequired(true).HasMaxLength(50);
            builder.Property(e => e.IsActive).IsRequired(true);
            builder.Property(e => e.InsertDate).IsRequired(true);

            builder.Property(e => e.CustomerId).IsRequired(true);
            builder.Property(e => e.Country).IsRequired(true).HasMaxLength(50);
            builder.Property(e => e.City).IsRequired(true).HasMaxLength(50);
            builder.Property(e => e.AddressLine).IsRequired(true).HasMaxLength(250);
            builder.Property(e => e.ZipCode).IsRequired(false).HasMaxLength(6);
            builder.Property(e => e.IsDefault).IsRequired(true);
        }

    }
}
