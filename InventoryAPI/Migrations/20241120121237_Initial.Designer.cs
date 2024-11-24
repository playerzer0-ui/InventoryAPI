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
    [Migration("20241120121237_Initial")]
    partial class Initial
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
                            Id = new Guid("f99ffff3-4990-46ff-976d-87a77f93cb8b"),
                            InvoiceDate = new DateOnly(2024, 10, 12),
                            OrderId = new Guid("123e290b-0c59-48d9-92aa-2d348961928e")
                        },
                        new
                        {
                            Id = new Guid("3dd5e586-fc6d-4d00-83de-e6226f2e52df"),
                            InvoiceDate = new DateOnly(2024, 10, 25),
                            OrderId = new Guid("b020b0e1-cdff-47a0-9c83-37437a870d91")
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
                            OrderId = new Guid("123e290b-0c59-48d9-92aa-2d348961928e"),
                            ProductId = new Guid("042e512b-6389-4ab1-83ff-e2f50a97afd9"),
                            Price = 34.649999999999999,
                            Quantity = 35
                        },
                        new
                        {
                            OrderId = new Guid("b020b0e1-cdff-47a0-9c83-37437a870d91"),
                            ProductId = new Guid("ce877c4d-6c14-46e2-a86f-e616755bde4f"),
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
                            Id = new Guid("123e290b-0c59-48d9-92aa-2d348961928e"),
                            OrderDate = new DateOnly(2024, 10, 12),
                            OrderType = "Out"
                        },
                        new
                        {
                            Id = new Guid("b020b0e1-cdff-47a0-9c83-37437a870d91"),
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
                            Id = new Guid("042e512b-6389-4ab1-83ff-e2f50a97afd9"),
                            Price = 0.98999999999999999,
                            ProductName = "Apple",
                            Quantity = 150
                        },
                        new
                        {
                            Id = new Guid("6a6dcd45-9493-40b1-8139-1b06cf0d4277"),
                            Price = 0.90000000000000002,
                            ProductName = "Pear",
                            Quantity = 75
                        },
                        new
                        {
                            Id = new Guid("56a6b8be-be0e-4311-b96c-728ba64ddb42"),
                            Price = 1.25,
                            ProductName = "Pineapple",
                            Quantity = 62
                        },
                        new
                        {
                            Id = new Guid("ce877c4d-6c14-46e2-a86f-e616755bde4f"),
                            Price = 1.5900000000000001,
                            ProductName = "Avocado",
                            Quantity = 100
                        },
                        new
                        {
                            Id = new Guid("98e96954-3901-4e8f-bdb9-44e527085cc5"),
                            Price = 0.98999999999999999,
                            ProductName = "Mango",
                            Quantity = 55
                        },
                        new
                        {
                            Id = new Guid("6692cd7d-c1c7-4946-8670-f59336d1f2cc"),
                            Price = 0.75,
                            ProductName = "Kiwi",
                            Quantity = 83
                        },
                        new
                        {
                            Id = new Guid("5a7f113d-8b4d-4c4b-a77e-f1bd2281adfc"),
                            Price = 2.0,
                            ProductName = "Banana",
                            Quantity = 90
                        },
                        new
                        {
                            Id = new Guid("6b2a98be-f2dc-4d39-885e-1ef0cd1470a0"),
                            Price = 0.62,
                            ProductName = "Orange",
                            Quantity = 70
                        },
                        new
                        {
                            Id = new Guid("c310515d-731b-45b3-bec2-2dbb6c128e3a"),
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
                            UsersId = new Guid("5a704bc3-673a-49ab-ba1e-390b80276547"),
                            Password = "123",
                            UserName = "admin",
                            UserType = 1
                        },
                        new
                        {
                            UsersId = new Guid("02883fa4-182b-466b-9dda-569b179e8e0d"),
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