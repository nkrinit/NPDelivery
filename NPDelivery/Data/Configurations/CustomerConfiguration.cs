using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using NPDelivery.Domain;

namespace NPDelivery.Data.Configurations;

public class CustomerConfiguration : IEntityTypeConfiguration<Customer>
{
    public void Configure(EntityTypeBuilder<Customer> builder)
    {
        builder.Property(x => x.Name).HasMaxLength(50);
        builder.Property(x => x.Surname).HasMaxLength(50);

        builder.HasData(new Customer("Nora", "Krinitskaya", "Shadow street, 24")
        {
            Id = 5,
        });
    }
}
