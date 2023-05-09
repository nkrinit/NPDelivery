using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using NPDelivery.Domain;

namespace NPDelivery.Data.Configurations;

public class ProductConfiguration : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        builder.Property(x => x.Name).HasMaxLength(200);
        builder.Property(x => x.Description).HasMaxLength(1000);

        builder.HasData(
            new Product("Chocolate bar", "White chocolate", 1100, 3)
            {
                Id = 4,
            },
            new Product("Chocolate bar", "Milk chocolate", 1100, 3)
            {
                Id = 41,
            },
            new Product("Chocolate bar", "Dark chocolate", 1100, 3)
            {
                Id = 42,
            }
        );
    }
}
