using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace BookStoreAPI.Migrations
{
    /// <inheritdoc />
    public partial class edit : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "29c21105-d6e4-4120-8e80-7c9511867ee1");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "cfdca6b6-4d0a-428e-9a25-674f4c727c46");

            migrationBuilder.RenameColumn(
                name: "srock",
                table: "Books",
                newName: "stock");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "035483ac-042f-4869-977c-b7a449085d99", null, "customer", "CUSTOMER" },
                    { "ddabf02b-5e36-47fe-a6dc-e06a99d2538a", null, "admin", "ADMIN" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "035483ac-042f-4869-977c-b7a449085d99");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "ddabf02b-5e36-47fe-a6dc-e06a99d2538a");

            migrationBuilder.RenameColumn(
                name: "stock",
                table: "Books",
                newName: "srock");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "29c21105-d6e4-4120-8e80-7c9511867ee1", null, "admin", "ADMIN" },
                    { "cfdca6b6-4d0a-428e-9a25-674f4c727c46", null, "customer", "CUSTOMER" }
                });
        }
    }
}
