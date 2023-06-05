using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using NPDelivery.Domain;

namespace NPDelivery.Data.Configurations;

public class CourierConfiguration : IEntityTypeConfiguration<Courier>
{
    public void Configure(EntityTypeBuilder<Courier> builder)
    {
        builder.Property(x => x.Name).HasMaxLength(50);
        builder.Property(x => x.Surname).HasMaxLength(50);
        builder.Property(x => x.Email).HasMaxLength(50);
        builder.Property(x => x.Phone).HasMaxLength(15);
        builder.Property(x => x.Remark).HasMaxLength(200);
    }
}
