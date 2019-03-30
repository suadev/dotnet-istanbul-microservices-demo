using Microsoft.EntityFrameworkCore;

namespace Services.Customers.Data
{
    public class CustomerDBContext : DbContext
    {
        public CustomerDBContext(DbContextOptions<CustomerDBContext> options) : base(options)
        {
        }
        public DbSet<Basket> Baskets { get; set; }
        public DbSet<BasketItem> BasketItems { get; set; }
        public DbSet<Customer> Customers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Basket>().ToTable("Basket");
            modelBuilder.Entity<Basket>().HasMany(x => x.Items);


            modelBuilder.Entity<BasketItem>().ToTable("BasketItem");

            modelBuilder.Entity<Customer>().ToTable("Customer");
        }
    }
}