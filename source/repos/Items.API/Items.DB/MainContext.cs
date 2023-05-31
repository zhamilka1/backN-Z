using Items.DB.Entities;
using Microsoft.EntityFrameworkCore;

namespace Items.DB
{
    public class MainContext : DbContext
    {
        public MainContext() { }
        public MainContext(DbContextOptions<MainContext> options)
        : base(options)
        {
            Database.EnsureCreated();
        }
        public DbSet<DbOrderItem> OrderItems { get; set; }
        public DbSet<DbShipmentItem> ShipmentItems { get; set; }
        public DbSet<DbItem> Items { get; set; }
        public DbSet<DbOrder> Orders { get; set; }
        public DbSet<DbShipment> Shipments { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            OnModelCreatingStatic(modelBuilder);
            base.OnModelCreating(modelBuilder);
        }

        public static void OnModelCreatingStatic(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<DbOrderItem>()
                .HasOne(oi => oi.Order)
                .WithMany()
                .HasForeignKey(k => k.OrderId)
                .IsRequired();
            modelBuilder.Entity<DbShipmentItem>()
                .HasOne(si => si.Shipment)
                .WithMany()
                .HasForeignKey(k => k.ShipmentId)
                .IsRequired();
            modelBuilder.Entity<DbOrderItem>()
                .HasOne(oi => oi.Item)
                .WithMany()
                .HasForeignKey(k => k.ItemId)
                .IsRequired();
            modelBuilder.Entity<DbShipmentItem>()
                .HasOne(si => si.Item)
                .WithMany()
                .HasForeignKey(k => k.ItemId)
                .IsRequired();
        }
    }
}