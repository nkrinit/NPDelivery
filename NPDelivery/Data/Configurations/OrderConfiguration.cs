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

        builder.HasData(new Order(5, 3, null, "Light street, 42", "Shadow street, 24")
        {
            Id = 6,
        });
    }

    public void Configure(EntityTypeBuilder<OrderedProduct> builder)
    {
        // todo: check this line
        //builder.HasNoKey();

        builder.HasData(
            new OrderedProduct(6, 4, 1),
            new OrderedProduct(6, 41, 1),
            new OrderedProduct(6, 42, 1)
        );
    }
}
