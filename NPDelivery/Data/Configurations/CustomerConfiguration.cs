using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using NPDelivery.Domain;

namespace NPDelivery.Data.Configurations;

public class CustomerConfiguration : IEntityTypeConfiguration<Customer>
{
    public void Configure(EntityTypeBuilder<Customer> builder)
    {
        builder.Property(x => x.Name).HasMaxLength(50);
        builder.Property(x => x.Surname).HasMaxLength(50);
        builder.Property(x => x.Address).HasMaxLength(100);
        builder.Property(x => x.Email).HasMaxLength(50);
        builder.Property(x => x.PhoneNumber).HasMaxLength(15);
        builder.Property(x => x.Remark).HasMaxLength(200);
    }
}
