using System.Text.Json;
using System.Text.Json.Serialization;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

using NPDelivery.Auth;
using NPDelivery.Domain;

namespace NPDelivery.Data.Configurations;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.Property(x => x.Email).HasMaxLength(128);
        builder.Property(x => x.Name).HasMaxLength(128);
        builder.Property(x => x.PasswordHash).HasMaxLength(200);

        var options = new JsonSerializerOptions(JsonSerializerDefaults.General);
        options.Converters.Add(new JsonStringEnumConverter());

        builder.Property(x => x.Roles).HasConversion(
            x => JsonSerializer.Serialize(x, options),
            o => JsonSerializer.Deserialize<List<ApplicationRole>>(o, options)!)
            .HasMaxLength(128);
    }
}
