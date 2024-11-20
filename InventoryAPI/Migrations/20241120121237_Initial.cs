using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace InventoryAPI.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Order",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    OrderDate = table.Column<DateOnly>(type: "date", nullable: false),
                    OrderType = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Order", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Product",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProductName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    Price = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Product", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    UsersId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserType = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.UsersId);
                });

            migrationBuilder.CreateTable(
                name: "Invoice",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    OrderId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    InvoiceDate = table.Column<DateOnly>(type: "date", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Invoice", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Invoice_Order_OrderId",
                        column: x => x.OrderId,
                        principalTable: "Order",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OrderProduct",
                columns: table => new
                {
                    OrderId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProductId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    Price = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderProduct", x => new { x.OrderId, x.ProductId });
                    table.ForeignKey(
                        name: "FK_OrderProduct_Order_OrderId",
                        column: x => x.OrderId,
                        principalTable: "Order",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OrderProduct_Product_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Product",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Order",
                columns: new[] { "Id", "OrderDate", "OrderType" },
                values: new object[,]
                {
                    { new Guid("123e290b-0c59-48d9-92aa-2d348961928e"), new DateOnly(2024, 10, 12), "Out" },
                    { new Guid("b020b0e1-cdff-47a0-9c83-37437a870d91"), new DateOnly(2024, 10, 25), "In" }
                });

            migrationBuilder.InsertData(
                table: "Product",
                columns: new[] { "Id", "Price", "ProductName", "Quantity" },
                values: new object[,]
                {
                    { new Guid("042e512b-6389-4ab1-83ff-e2f50a97afd9"), 0.98999999999999999, "Apple", 150 },
                    { new Guid("56a6b8be-be0e-4311-b96c-728ba64ddb42"), 1.25, "Pineapple", 62 },
                    { new Guid("5a7f113d-8b4d-4c4b-a77e-f1bd2281adfc"), 2.0, "Banana", 90 },
                    { new Guid("6692cd7d-c1c7-4946-8670-f59336d1f2cc"), 0.75, "Kiwi", 83 },
                    { new Guid("6a6dcd45-9493-40b1-8139-1b06cf0d4277"), 0.90000000000000002, "Pear", 75 },
                    { new Guid("6b2a98be-f2dc-4d39-885e-1ef0cd1470a0"), 0.62, "Orange", 70 },
                    { new Guid("98e96954-3901-4e8f-bdb9-44e527085cc5"), 0.98999999999999999, "Mango", 55 },
                    { new Guid("c310515d-731b-45b3-bec2-2dbb6c128e3a"), 0.58999999999999997, "Passion fruit", 120 },
                    { new Guid("ce877c4d-6c14-46e2-a86f-e616755bde4f"), 1.5900000000000001, "Avocado", 100 }
                });

            migrationBuilder.InsertData(
                table: "User",
                columns: new[] { "UsersId", "Password", "UserName", "UserType" },
                values: new object[,]
                {
                    { new Guid("02883fa4-182b-466b-9dda-569b179e8e0d"), "123", "supplier", 0 },
                    { new Guid("5a704bc3-673a-49ab-ba1e-390b80276547"), "123", "admin", 1 }
                });

            migrationBuilder.InsertData(
                table: "Invoice",
                columns: new[] { "Id", "InvoiceDate", "OrderId" },
                values: new object[,]
                {
                    { new Guid("3dd5e586-fc6d-4d00-83de-e6226f2e52df"), new DateOnly(2024, 10, 25), new Guid("b020b0e1-cdff-47a0-9c83-37437a870d91") },
                    { new Guid("f99ffff3-4990-46ff-976d-87a77f93cb8b"), new DateOnly(2024, 10, 12), new Guid("123e290b-0c59-48d9-92aa-2d348961928e") }
                });

            migrationBuilder.InsertData(
                table: "OrderProduct",
                columns: new[] { "OrderId", "ProductId", "Price", "Quantity" },
                values: new object[,]
                {
                    { new Guid("123e290b-0c59-48d9-92aa-2d348961928e"), new Guid("042e512b-6389-4ab1-83ff-e2f50a97afd9"), 34.649999999999999, 35 },
                    { new Guid("b020b0e1-cdff-47a0-9c83-37437a870d91"), new Guid("ce877c4d-6c14-46e2-a86f-e616755bde4f"), 79.5, 50 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Invoice_OrderId",
                table: "Invoice",
                column: "OrderId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_OrderProduct_ProductId",
                table: "OrderProduct",
                column: "ProductId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Invoice");

            migrationBuilder.DropTable(
                name: "OrderProduct");

            migrationBuilder.DropTable(
                name: "User");

            migrationBuilder.DropTable(
                name: "Order");

            migrationBuilder.DropTable(
                name: "Product");
        }
    }
}
