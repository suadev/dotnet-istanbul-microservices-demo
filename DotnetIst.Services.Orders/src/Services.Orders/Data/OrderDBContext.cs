using Microsoft.EntityFrameworkCore;
using Services.Orders.Data;

namespace Services.Customers.Data
{
    public class OrderDBContext : DbContext
    {
        public OrderDBContext(DbContextOptions<OrderDBContext> options) : base(options)
        {
        }
        public DbSet<Order> Orders { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Order>().ToTable("Order");
            modelBuilder.Entity<Order>().HasMany(x => x.Items);
        }
    }
}