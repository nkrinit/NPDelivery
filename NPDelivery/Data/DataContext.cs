using Microsoft.EntityFrameworkCore;

using NPDelivery.Domain;

namespace NPDelivery.Data;

public class DataContext : DbContext
{
    public DbSet<Customer> Customers { get; set; }
    public DbSet<Order> Orders { get; set; }
    public DbSet<Product> Products { get; set; }
    public DbSet<OrderedProduct> OrderedProducts { get; set; }
    public DbSet<Store> Stores { get; set; }
    public DbSet<StoreKeeper> StoreKeepers { get; set; }
    //public DbSet<Address> Addresses { get; set; }

    public DbSet<Courier> Couriers { get; set; }
    public DbSet<Shift> Shifts { get; set; }

    public DbSet<User> Users { get; set; }

    public DataContext(DbContextOptions dbContextOptions) : base(dbContextOptions)
    {

    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(DataContext).Assembly);
        modelBuilder.UseHiLo();
    }
}
