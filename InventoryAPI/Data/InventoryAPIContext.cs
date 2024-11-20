using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using InventoryAPI.Models;
using Microsoft.CodeAnalysis;

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

            Guid productId1 = Guid.NewGuid();
            Guid productId2 = Guid.NewGuid();
            Guid productId3 = Guid.NewGuid();
            Guid productId4 = Guid.NewGuid();
            Guid productId5 = Guid.NewGuid();
            Guid productId6 = Guid.NewGuid();
            Guid productId7 = Guid.NewGuid();
            Guid productId8 = Guid.NewGuid();
            Guid productId9 = Guid.NewGuid();

            Guid orderId1 = Guid.NewGuid();
            Guid orderId2 = Guid.NewGuid();

            modelBuilder.Entity<Users>().HasData(
                new Users { UsersId = Guid.NewGuid(), UserName = "admin", Password = "123", UserType = 1 },
                new Users { UsersId = Guid.NewGuid(), UserName = "supplier", Password = "123", UserType = 0 }

            );

            modelBuilder.Entity<Products>().HasData(
                new Products { Id = productId1, ProductName = "Apple", Quantity = 150, Price = 0.99 },
                new Products { Id = productId2, ProductName = "Pear", Quantity = 75, Price = 0.90 },
                new Products { Id = productId3, ProductName = "Pineapple", Quantity = 62, Price = 1.25 },
                new Products { Id = productId4, ProductName = "Avocado", Quantity = 100, Price = 1.59 },
                new Products { Id = productId5, ProductName = "Mango", Quantity = 55, Price = 0.99 },
                new Products { Id = productId6, ProductName = "Kiwi", Quantity = 83, Price = 0.75 },
                new Products { Id = productId7, ProductName = "Banana", Quantity = 90, Price = 2.00 },
                new Products { Id = productId8, ProductName = "Orange", Quantity = 70, Price = 0.62 },
                new Products { Id = productId9, ProductName = "Passion fruit", Quantity = 120, Price = 0.59 }

            );

            modelBuilder.Entity<Orders>().HasData(
               new Orders { Id = orderId1, OrderDate = DateOnly.Parse("2024-10-12"), OrderType = "Out" },
               new Orders { Id = orderId2, OrderDate = DateOnly.Parse("2024-10-25"), OrderType = "In" }

           );

            modelBuilder.Entity<OrderProducts>().HasData(
               new OrderProducts { OrderId = orderId1, ProductId = productId1, Quantity = 35, Price = 34.65 },
               new OrderProducts { OrderId = orderId2, ProductId = productId4, Quantity = 50, Price = 79.50 }

           );

            modelBuilder.Entity<Invoices>().HasData(
               new Invoices { Id = Guid.NewGuid(), OrderId = orderId1, InvoiceDate = DateOnly.Parse("2024-10-12") },
               new Invoices { Id = Guid.NewGuid(), OrderId = orderId2, InvoiceDate = DateOnly.Parse("2024-10-25") }

           );
        }
    }
}
