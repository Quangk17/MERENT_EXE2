using Domain.Entites;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;


namespace Infrastructures
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }
        public DbSet<Wallet> Wallets { get; set; }
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
        public DbSet<Transaction> Transactions { get; set;}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            foreach (var relationship in modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
            {
                relationship.DeleteBehavior = DeleteBehavior.Restrict;
            }

            // Configure entity relationships
            // Product Of Store
            modelBuilder.Entity<ProductOfStore>()
            .HasKey(ps => new { ps.StoreID, ps.ProductID});

            modelBuilder.Entity<ProductOfStore>()
            .HasOne(s => s.Store)
            .WithMany(ps => ps.ProductOfStores)
            .HasForeignKey(s => s.StoreID)
            .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<ProductOfStore>()
            .HasOne(p=>p.Product)
            .WithMany(ps => ps.ProductOfStores)
            .HasForeignKey(s => s.ProductID)
            .OnDelete(DeleteBehavior.Restrict);

            // Combo Of Product
            modelBuilder.Entity<ComboOfProduct>()
            .HasKey(cp => new {cp.ComboID, cp.ProductID});

            modelBuilder.Entity<ComboOfProduct>()
            .HasOne(p => p.Combo)
            .WithMany(cp => cp.ComboOfProducts)
            .HasForeignKey(c => c.ComboID)
            .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<ComboOfProduct>()
            .HasOne(p => p.Product)
            .WithMany(cp => cp.ComboOfProducts)
            .HasForeignKey(p => p.ProductID)
            .OnDelete(DeleteBehavior.Restrict);

            // Service Of Store

            modelBuilder.Entity<ServiceOfStore>()
            .HasKey(ss => new { ss.ServiceID, ss.StoreID });

            modelBuilder.Entity<ServiceOfStore>()
            .HasOne(p => p.Service)
            .WithMany(ss => ss.ServiceOfStores)
            .HasForeignKey(ss => ss.ServiceID)
            .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<ServiceOfStore>()
            .HasOne(p => p.Store)
            .WithMany(ss => ss.ServiceOfStores)
            .HasForeignKey(s => s.StoreID)
            .OnDelete(DeleteBehavior.Restrict);

            // Configure ServiceOrder
            modelBuilder.Entity<ServiceOrder>()
                .HasOne(so => so.User)
                .WithMany(u => u.ServiceOrders)
                .HasForeignKey(so => so.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            // Configure ServiceOrderDetail

            modelBuilder.Entity<ServiceOrderDetail>()
            .HasKey(sod => new {sod.ServiceID, sod.ServiceOrderID});

            modelBuilder.Entity<ServiceOrderDetail>()
                .HasOne(sod => sod.ServiceOrder)
                .WithMany(so => so.ServiceOrderDetails)
                .HasForeignKey(sod => sod.ServiceOrderID)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<ServiceOrderDetail>()
                .HasOne(sod => sod.Service)
                .WithMany(s => s.ServiceOrderDetails)
                .HasForeignKey(sod => sod.ServiceID)
                .OnDelete(DeleteBehavior.Restrict);
            // Product Order

            modelBuilder.Entity<ProductOrder>()
            .HasKey(po => po.Id);

            modelBuilder.Entity<ProductOrder>()
            .HasOne(u => u.User)
            .WithMany(po => po.ProductOrders)
            .HasForeignKey(po => po.UserID)
            .OnDelete(DeleteBehavior.Restrict);

            // Product Order Details

            modelBuilder.Entity<ProductOrderDetails>()
            .HasKey(pod => new { pod.ProductID, pod.OrderId });

            modelBuilder.Entity<ProductOrderDetails>()
            .HasOne(po => po.ProductOrder)
            .WithMany(pod => pod.ProductOrderDetails)
            .HasForeignKey(pod => pod.OrderId)
            .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<ProductOrderDetails>()
            .HasOne(p => p.Product)
            .WithMany(pod => pod.ProductOrderDetails)
            .HasForeignKey(pod => pod.ProductID)
            .OnDelete(DeleteBehavior.Restrict);

            // Configure User

            modelBuilder.Entity<User>()
            .HasOne(u => u.Role)
            .WithMany(r => r.Users)
            .HasForeignKey(r => r.RoleID)
            .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Wallet>()
             .HasOne(w => w.User)
             .WithOne(u => u.Wallets)
             .HasForeignKey<Wallet>(u => u.UserId);

            modelBuilder.Entity<Transaction>()
             .HasOne(w => w.Wallets)
             .WithMany(u => u.Transactions)
             .HasForeignKey(u => u.WalletId);
        }
    }

}
