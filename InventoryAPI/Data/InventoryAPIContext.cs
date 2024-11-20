using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using InventoryAPI.Models;

namespace InventoryAPI.Data
{
    public class InventoryAPIContext : DbContext
    {
        public InventoryAPIContext (DbContextOptions<InventoryAPIContext> options)
            : base(options)
        {
        }

        public DbSet<Products> Product { get; set; } = default!;
        public DbSet<Users> User { get; set; } = default!;
        public DbSet<Orders> Order { get; set; } = default!;
        public DbSet<Invoices> Invoice { get; set; } = default!;
        public DbSet<OrderProducts> OrderProduct { get; set; } = default!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Composite key for OrderProduct (many-to-many relationship)
            modelBuilder.Entity<OrderProducts>()
                .HasKey(op => new { op.OrderId, op.ProductId });

            // Many-to-many: Order to Products (through OrderProduct)
            modelBuilder.Entity<OrderProducts>()
                .HasOne(op => op.Order)
                .WithMany(o => o.OrderProducts)
                .HasForeignKey(op => op.OrderId);

            modelBuilder.Entity<OrderProducts>()
                .HasOne(op => op.Product)
                .WithMany(p => p.OrderProducts)
                .HasForeignKey(op => op.ProductId);

            // One-to-one: Order to Invoice
            modelBuilder.Entity<Orders>()
                .HasOne(o => o.Invoice)
                .WithOne(i => i.Order)
                .HasForeignKey<Invoices>(i => i.OrderId);

            /*modelBuilder.Entity<Users>().HasData(
                new Users { UsersId = Guid.NewGuid(), UserName = "admin", Password = "123", UserType = 1 },
                new Users { UsersId = Guid.NewGuid(), UserName = "supplier", Password = "123", UserType = 0 }
            );*/
        }
    }
}
