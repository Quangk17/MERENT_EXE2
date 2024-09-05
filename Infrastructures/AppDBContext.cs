using Domain.Entites;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;


namespace Infrastructures
{
    public class AppDbContext : IdentityDbContext<User, Role, int>
    {
        public AppDbContext(DbContextOptions options) : base(options)
        {
        }
        public DbSet<Wallets> Wallets { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Store> Stores { get; set; }
        public DbSet<ServiceOrderDetail> ServiceOrderDetails { get; set; }
        public DbSet<ServiceOrder> ServiceOrders { get; set; }
        public DbSet<ServiceOfStore> ServiceOfStores { get; set; }
        public DbSet<Service> Services { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<ProductOrderDetails> ProductOrderDetails { get; set; }
        public DbSet<ProductOrder> ProductOrders { get; set; }
        public DbSet<ProductOfStore> ProductOfStores { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ComboOfProduct> ComboOfProducts { get; set; }
        public DbSet<Combo> Combos { get; set; }



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            foreach (var relationship in modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
            {
                relationship.DeleteBehavior = DeleteBehavior.Restrict;
            }

            // Configure entity relationships

            modelBuilder.Entity<ProductOfStore>()
            .HasKey(ps => new { ps.StoreID, ps.ProductID});

            modelBuilder.Entity<ProductOfStore>()
            .HasOne(s => s.Store)
            .WithMany(ps => ps.ProductOfStores)
            .OnDelete(DeleteBehavior.Restrict);

        }
    }

}
