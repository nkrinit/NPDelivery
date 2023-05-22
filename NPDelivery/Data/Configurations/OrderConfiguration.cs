using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using NPDelivery.Domain;

namespace NPDelivery.Data.Configurations;

public class OrderConfiguration : IEntityTypeConfiguration<Order>, IEntityTypeConfiguration<OrderedProduct>
{
    public void Configure(EntityTypeBuilder<Order> builder)
    {
        builder
            .HasMany(x => x.Products)
            .WithMany(x => x.Orders)
            .UsingEntity<OrderedProduct>();
    }

    public void Configure(EntityTypeBuilder<OrderedProduct> builder)
    {
        // todo: check this line
        //builder.HasNoKey();
    }
}
