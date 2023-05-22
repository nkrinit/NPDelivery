using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using NPDelivery.Domain;

namespace NPDelivery.Data.Configurations;

public class StoreConfiguration : IEntityTypeConfiguration<Store>, IEntityTypeConfiguration<StoreKeeper>
{
    public void Configure(EntityTypeBuilder<StoreKeeper> builder)
    {
        builder
            .HasMany(x => x.Stores)
            .WithOne(x => x.StoreKeeper);
    }

    public void Configure(EntityTypeBuilder<Store> builder)
    {
        builder.Property(x => x.Name).HasMaxLength(200);
        builder.Property(x => x.Description).HasMaxLength(1000);
    }
}
