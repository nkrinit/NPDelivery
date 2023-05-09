using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using NPDelivery.Domain;

namespace NPDelivery.Data.Configurations;

//public class AddressConfiguration : IEntityTypeConfiguration<Address>
//{
//    public void Configure(EntityTypeBuilder<Address> builder)
//    {
//        builder.Property(x => x.City).HasMaxLength(100);
//        builder.Property(x => x.Street).HasMaxLength(200);
//        builder.Property(x => x.Comment).HasMaxLength(1000);

//        builder.HasData(
//            new Address("Odessa", "Shadow street, 24")
//            {
//                Id = 1
//            },
//            new Address("Odessa", "Light street, 42")
//            {
//                Id = 11
//            }
//        );
//    }
//}
