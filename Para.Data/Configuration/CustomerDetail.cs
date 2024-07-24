using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Para.Data.Domain;

namespace Para.Data.Configuration
{
    public class CustomerDetailConfiguration : IEntityTypeConfiguration<CustomerDetail>
    {
        public void Configure(EntityTypeBuilder<CustomerDetail> builder)
        {
            builder.Property(e => e.InsertUser).IsRequired(true).HasMaxLength(50);
            builder.Property(e => e.IsActive).IsRequired(true);
            builder.Property(e => e.InsertDate).IsRequired(true);

            builder.Property(e => e.FatherName).IsRequired(true).HasMaxLength(50);
            builder.Property(e => e.MotherName).IsRequired(true).HasMaxLength(50);
            builder.Property(e => e.MountlyIncome).IsRequired(true).HasMaxLength(50);
            builder.Property(e => e.Occupation).IsRequired(true).HasMaxLength(50);
            builder.Property(e => e.EducationStatus).IsRequired(true).HasMaxLength(50);

        }

    }
}
