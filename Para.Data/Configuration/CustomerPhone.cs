using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Para.Data.Domain;

namespace Para.Data.Configuration
{
    public class CustomerPhoneConfiguration : IEntityTypeConfiguration<CustomerPhone>
    {
        public void Configure(EntityTypeBuilder<CustomerPhone> builder)
        {
            builder.Property(e => e.InsertUser).IsRequired(true).HasMaxLength(50);
            builder.Property(e => e.IsActive).IsRequired(true);
            builder.Property(e => e.InsertDate).IsRequired(true);

            builder.Property(e => e.CustomerId).IsRequired(true);
            builder.Property(e => e.CountryCode).IsRequired(true).HasMaxLength(3);
            builder.Property(e => e.Phone).IsRequired(true).HasMaxLength(10);
            builder.Property(e => e.IsDefault).IsRequired(true);

            builder.HasIndex(x => new { x.CountryCode, x.Phone }).IsUnique(true);
            builder.HasIndex(x => new { x.CountryCode, x.Phone }).IsUnique(true);

        }

    }
}
