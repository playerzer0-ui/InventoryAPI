﻿// <auto-generated />
using System;
using InventoryAPI.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace InventoryAPI.Migrations
{
    [DbContext(typeof(InventoryAPIContext))]
    [Migration("20241120110118_SeedData")]
    partial class SeedData
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("InventoryAPI.Models.Invoices", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateOnly>("InvoiceDate")
                        .HasColumnType("date");

                    b.Property<Guid>("OrderId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("OrderId")
                        .IsUnique();

                    b.ToTable("Invoice");

                    b.HasData(
                        new
                        {
                            Id = new Guid("2f2f140a-b5e4-4f48-a63f-3ee2c3b62492"),
                            InvoiceDate = new DateOnly(2024, 10, 12),
                            OrderId = new Guid("b118dbdc-f569-42ec-9dbc-ea91485dd606")
                        },
                        new
                        {
                            Id = new Guid("c3074d29-147a-405f-a6dd-f8208d5ee5ca"),
                            InvoiceDate = new DateOnly(2024, 10, 25),
                            OrderId = new Guid("e31bd55d-14ad-44b3-bb34-2639e6bcff9b")
                        });
                });

            modelBuilder.Entity("InventoryAPI.Models.OrderProducts", b =>
                {
                    b.Property<Guid>("OrderId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("ProductId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<double>("Price")
                        .HasColumnType("float");

                    b.Property<int>("Quantity")
                        .HasColumnType("int");

                    b.HasKey("OrderId", "ProductId");

                    b.HasIndex("ProductId");

                    b.ToTable("OrderProduct");

                    b.HasData(
                        new
                        {
                            OrderId = new Guid("b118dbdc-f569-42ec-9dbc-ea91485dd606"),
                            ProductId = new Guid("8ff68a10-9e11-4154-9b49-32378b295965"),
                            Price = 34.649999999999999,
                            Quantity = 35
                        },
                        new
                        {
                            OrderId = new Guid("e31bd55d-14ad-44b3-bb34-2639e6bcff9b"),
                            ProductId = new Guid("c74b4433-8ee0-4dee-80c8-8d7ba4252c9e"),
                            Price = 79.5,
                            Quantity = 50
                        });
                });

            modelBuilder.Entity("InventoryAPI.Models.Orders", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateOnly>("OrderDate")
                        .HasColumnType("date");

                    b.Property<string>("OrderType")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Order");

                    b.HasData(
                        new
                        {
                            Id = new Guid("b118dbdc-f569-42ec-9dbc-ea91485dd606"),
                            OrderDate = new DateOnly(2024, 10, 12),
                            OrderType = "Out"
                        },
                        new
                        {
                            Id = new Guid("e31bd55d-14ad-44b3-bb34-2639e6bcff9b"),
                            OrderDate = new DateOnly(2024, 10, 25),
                            OrderType = "In"
                        });
                });

            modelBuilder.Entity("InventoryAPI.Models.Products", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<double>("Price")
                        .HasColumnType("float");

                    b.Property<string>("ProductName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Quantity")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Product");

                    b.HasData(
                        new
                        {
                            Id = new Guid("8ff68a10-9e11-4154-9b49-32378b295965"),
                            Price = 0.98999999999999999,
                            ProductName = "Apple",
                            Quantity = 150
                        },
                        new
                        {
                            Id = new Guid("bc8c5d44-4d42-456a-927e-294bf5959a9f"),
                            Price = 0.90000000000000002,
                            ProductName = "Pear",
                            Quantity = 75
                        },
                        new
                        {
                            Id = new Guid("4d475875-34e7-4a37-9bd5-5bb843636271"),
                            Price = 1.25,
                            ProductName = "Pineapple",
                            Quantity = 62
                        },
                        new
                        {
                            Id = new Guid("c74b4433-8ee0-4dee-80c8-8d7ba4252c9e"),
                            Price = 1.5900000000000001,
                            ProductName = "Avocado",
                            Quantity = 100
                        },
                        new
                        {
                            Id = new Guid("38033716-440d-4bfb-abb2-58d54bd6d33b"),
                            Price = 0.98999999999999999,
                            ProductName = "Mango",
                            Quantity = 55
                        },
                        new
                        {
                            Id = new Guid("22fccc14-8603-4f03-b70c-a19ed34c72f5"),
                            Price = 0.75,
                            ProductName = "Kiwi",
                            Quantity = 83
                        },
                        new
                        {
                            Id = new Guid("2c5e55fd-2ccd-4f48-bd58-07a9b5a3410a"),
                            Price = 2.0,
                            ProductName = "Banana",
                            Quantity = 90
                        },
                        new
                        {
                            Id = new Guid("993d0306-edf4-4b99-a358-d938ac71591c"),
                            Price = 0.62,
                            ProductName = "Orange",
                            Quantity = 70
                        },
                        new
                        {
                            Id = new Guid("fb567534-309b-4079-98eb-d8206e9611e3"),
                            Price = 0.58999999999999997,
                            ProductName = "Passion fruit",
                            Quantity = 120
                        });
                });

            modelBuilder.Entity("InventoryAPI.Models.Users", b =>
                {
                    b.Property<Guid>("UsersId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("UserType")
                        .HasColumnType("int");

                    b.HasKey("UsersId");

                    b.ToTable("User");

                    b.HasData(
                        new
                        {
                            UsersId = new Guid("65e68361-a1e8-499b-9f59-7b0a8eb4c904"),
                            Password = "123",
                            UserName = "admin",
                            UserType = 1
                        },
                        new
                        {
                            UsersId = new Guid("d62f6c2c-f56a-46ea-85d1-4949cc55f73e"),
                            Password = "123",
                            UserName = "supplier",
                            UserType = 0
                        });
                });

            modelBuilder.Entity("InventoryAPI.Models.Invoices", b =>
                {
                    b.HasOne("InventoryAPI.Models.Orders", "Order")
                        .WithOne("Invoice")
                        .HasForeignKey("InventoryAPI.Models.Invoices", "OrderId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Order");
                });

            modelBuilder.Entity("InventoryAPI.Models.OrderProducts", b =>
                {
                    b.HasOne("InventoryAPI.Models.Orders", "Order")
                        .WithMany("OrderProducts")
                        .HasForeignKey("OrderId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("InventoryAPI.Models.Products", "Product")
                        .WithMany("OrderProducts")
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Order");

                    b.Navigation("Product");
                });

            modelBuilder.Entity("InventoryAPI.Models.Orders", b =>
                {
                    b.Navigation("Invoice");

                    b.Navigation("OrderProducts");
                });

            modelBuilder.Entity("InventoryAPI.Models.Products", b =>
                {
                    b.Navigation("OrderProducts");
                });
#pragma warning restore 612, 618
        }
    }
}
