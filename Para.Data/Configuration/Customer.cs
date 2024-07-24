using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Para.Data.Domain;

namespace Para.Data.Configuration
{
    public class CustomerConfiguration : IEntityTypeConfiguration<Customer>
    {
        public void Configure(EntityTypeBuilder<Customer> builder)
        {
            builder.Property(e => e.InsertUser).IsRequired(true).HasMaxLength(50);
            builder.Property(e => e.IsActive).IsRequired(true);
            builder.Property(e => e.InsertDate).IsRequired(true);

            builder.Property(e => e.FirstName).IsRequired(true).HasMaxLength(50);
            builder.Property(e => e.LastName).IsRequired(true).HasMaxLength(50);
            builder.Property(e => e.IdentityNumber).IsRequired(true).HasMaxLength(11);
            builder.Property(e => e.Email).IsRequired(true).HasMaxLength(100);
            builder.Property(e => e.CustomerNumber).IsRequired(true);
            builder.Property(e => e.DateOfBirth).IsRequired(true);

            builder.HasIndex(x => x.IdentityNumber).IsUnique(true);
            builder.HasIndex(x => x.Email).IsUnique(true);
            builder.HasIndex(x => x.CustomerNumber).IsUnique(true);


            builder.HasMany(x => x.CustomerAddresses)
                .WithOne(x => x.Customer)
                .HasForeignKey(x => x.CustomerId)
                .IsRequired(true)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(x => x.CustomerPhones)
                .WithOne(x => x.Customer)
                .HasForeignKey(x => x.CustomerId)
                .IsRequired(true)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(x => x.CustomerDetail)
                .WithOne(x => x.Customer)
                .HasForeignKey<CustomerDetail>(x => x.CustomerId)
                .IsRequired(true)
                .OnDelete(DeleteBehavior.Cascade);

        }

    }
}
