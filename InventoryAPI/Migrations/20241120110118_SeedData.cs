using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace InventoryAPI.Migrations
{
    /// <inheritdoc />
    public partial class SeedData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Order",
                columns: new[] { "Id", "OrderDate", "OrderType" },
                values: new object[,]
                {
                    { new Guid("b118dbdc-f569-42ec-9dbc-ea91485dd606"), new DateOnly(2024, 10, 12), "Out" },
                    { new Guid("e31bd55d-14ad-44b3-bb34-2639e6bcff9b"), new DateOnly(2024, 10, 25), "In" }
                });

            migrationBuilder.InsertData(
                table: "Product",
                columns: new[] { "Id", "Price", "ProductName", "Quantity" },
                values: new object[,]
                {
                    { new Guid("22fccc14-8603-4f03-b70c-a19ed34c72f5"), 0.75, "Kiwi", 83 },
                    { new Guid("2c5e55fd-2ccd-4f48-bd58-07a9b5a3410a"), 2.0, "Banana", 90 },
                    { new Guid("38033716-440d-4bfb-abb2-58d54bd6d33b"), 0.98999999999999999, "Mango", 55 },
                    { new Guid("4d475875-34e7-4a37-9bd5-5bb843636271"), 1.25, "Pineapple", 62 },
                    { new Guid("8ff68a10-9e11-4154-9b49-32378b295965"), 0.98999999999999999, "Apple", 150 },
                    { new Guid("993d0306-edf4-4b99-a358-d938ac71591c"), 0.62, "Orange", 70 },
                    { new Guid("bc8c5d44-4d42-456a-927e-294bf5959a9f"), 0.90000000000000002, "Pear", 75 },
                    { new Guid("c74b4433-8ee0-4dee-80c8-8d7ba4252c9e"), 1.5900000000000001, "Avocado", 100 },
                    { new Guid("fb567534-309b-4079-98eb-d8206e9611e3"), 0.58999999999999997, "Passion fruit", 120 }
                });

            migrationBuilder.InsertData(
                table: "User",
                columns: new[] { "UsersId", "Password", "UserName", "UserType" },
                values: new object[,]
                {
                    { new Guid("65e68361-a1e8-499b-9f59-7b0a8eb4c904"), "123", "admin", 1 },
                    { new Guid("d62f6c2c-f56a-46ea-85d1-4949cc55f73e"), "123", "supplier", 0 }
                });

            migrationBuilder.InsertData(
                table: "Invoice",
                columns: new[] { "Id", "InvoiceDate", "OrderId" },
                values: new object[,]
                {
                    { new Guid("2f2f140a-b5e4-4f48-a63f-3ee2c3b62492"), new DateOnly(2024, 10, 12), new Guid("b118dbdc-f569-42ec-9dbc-ea91485dd606") },
                    { new Guid("c3074d29-147a-405f-a6dd-f8208d5ee5ca"), new DateOnly(2024, 10, 25), new Guid("e31bd55d-14ad-44b3-bb34-2639e6bcff9b") }
                });

            migrationBuilder.InsertData(
                table: "OrderProduct",
                columns: new[] { "OrderId", "ProductId", "Price", "Quantity" },
                values: new object[,]
                {
                    { new Guid("b118dbdc-f569-42ec-9dbc-ea91485dd606"), new Guid("8ff68a10-9e11-4154-9b49-32378b295965"), 34.649999999999999, 35 },
                    { new Guid("e31bd55d-14ad-44b3-bb34-2639e6bcff9b"), new Guid("c74b4433-8ee0-4dee-80c8-8d7ba4252c9e"), 79.5, 50 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Invoice",
                keyColumn: "Id",
                keyValue: new Guid("2f2f140a-b5e4-4f48-a63f-3ee2c3b62492"));

            migrationBuilder.DeleteData(
                table: "Invoice",
                keyColumn: "Id",
                keyValue: new Guid("c3074d29-147a-405f-a6dd-f8208d5ee5ca"));

            migrationBuilder.DeleteData(
                table: "OrderProduct",
                keyColumns: new[] { "OrderId", "ProductId" },
                keyValues: new object[] { new Guid("b118dbdc-f569-42ec-9dbc-ea91485dd606"), new Guid("8ff68a10-9e11-4154-9b49-32378b295965") });

            migrationBuilder.DeleteData(
                table: "OrderProduct",
                keyColumns: new[] { "OrderId", "ProductId" },
                keyValues: new object[] { new Guid("e31bd55d-14ad-44b3-bb34-2639e6bcff9b"), new Guid("c74b4433-8ee0-4dee-80c8-8d7ba4252c9e") });

            migrationBuilder.DeleteData(
                table: "Product",
                keyColumn: "Id",
                keyValue: new Guid("22fccc14-8603-4f03-b70c-a19ed34c72f5"));

            migrationBuilder.DeleteData(
                table: "Product",
                keyColumn: "Id",
                keyValue: new Guid("2c5e55fd-2ccd-4f48-bd58-07a9b5a3410a"));

            migrationBuilder.DeleteData(
                table: "Product",
                keyColumn: "Id",
                keyValue: new Guid("38033716-440d-4bfb-abb2-58d54bd6d33b"));

            migrationBuilder.DeleteData(
                table: "Product",
                keyColumn: "Id",
                keyValue: new Guid("4d475875-34e7-4a37-9bd5-5bb843636271"));

            migrationBuilder.DeleteData(
                table: "Product",
                keyColumn: "Id",
                keyValue: new Guid("993d0306-edf4-4b99-a358-d938ac71591c"));

            migrationBuilder.DeleteData(
                table: "Product",
                keyColumn: "Id",
                keyValue: new Guid("bc8c5d44-4d42-456a-927e-294bf5959a9f"));

            migrationBuilder.DeleteData(
                table: "Product",
                keyColumn: "Id",
                keyValue: new Guid("fb567534-309b-4079-98eb-d8206e9611e3"));

            migrationBuilder.DeleteData(
                table: "User",
                keyColumn: "UsersId",
                keyValue: new Guid("65e68361-a1e8-499b-9f59-7b0a8eb4c904"));

            migrationBuilder.DeleteData(
                table: "User",
                keyColumn: "UsersId",
                keyValue: new Guid("d62f6c2c-f56a-46ea-85d1-4949cc55f73e"));

            migrationBuilder.DeleteData(
                table: "Order",
                keyColumn: "Id",
                keyValue: new Guid("b118dbdc-f569-42ec-9dbc-ea91485dd606"));

            migrationBuilder.DeleteData(
                table: "Order",
                keyColumn: "Id",
                keyValue: new Guid("e31bd55d-14ad-44b3-bb34-2639e6bcff9b"));

            migrationBuilder.DeleteData(
                table: "Product",
                keyColumn: "Id",
                keyValue: new Guid("8ff68a10-9e11-4154-9b49-32378b295965"));

            migrationBuilder.DeleteData(
                table: "Product",
                keyColumn: "Id",
                keyValue: new Guid("c74b4433-8ee0-4dee-80c8-8d7ba4252c9e"));
        }
    }
}
