using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace MerchantProfile.Api.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Merchants",
                keyColumn: "Id",
                keyValue: "39ae71a3-1f29-4c5e-a0e8-12e06b70f7b5");

            migrationBuilder.DeleteData(
                table: "Merchants",
                keyColumn: "Id",
                keyValue: "e9b10673-5bf5-4c7c-be43-49740b7c6395");

            migrationBuilder.DropColumn(
                name: "signature",
                table: "Merchants");

            migrationBuilder.AddColumn<string>(
                name: "Username",
                table: "Merchants",
                type: "longtext",
                nullable: false);

            migrationBuilder.InsertData(
                table: "Merchants",
                columns: new[] { "Id", "Address", "Description", "Email", "LinkWebsite", "Name", "Password", "Phone", "Username" },
                values: new object[,]
                {
                    { "19c7cda1-95ad-4d09-9ffa-89143f6c21eb", "456 Elm St, Othertown, USA", "Second Merchant Description", "sample2@gmail.com", "http://www.merchanttwo.com", "Merchant Two", "20ur290ur2ur8dd3d324r2r2r2", "098-765-4321", "hiepth321" },
                    { "69f5b439-4067-42f5-b9c6-dd63b4a7c5d2", "123 Main St, Anytown, USA", "First Merchant Description", "sample@gmail.com", "http://www.merchantone.com", "Merchant One", "20ur290ur2ur824r2r2r2", "123-456-7890", "hiepth123" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Merchants",
                keyColumn: "Id",
                keyValue: "19c7cda1-95ad-4d09-9ffa-89143f6c21eb");

            migrationBuilder.DeleteData(
                table: "Merchants",
                keyColumn: "Id",
                keyValue: "69f5b439-4067-42f5-b9c6-dd63b4a7c5d2");

            migrationBuilder.DropColumn(
                name: "Username",
                table: "Merchants");

            migrationBuilder.AddColumn<string>(
                name: "signature",
                table: "Merchants",
                type: "longtext",
                nullable: true);

            migrationBuilder.InsertData(
                table: "Merchants",
                columns: new[] { "Id", "Address", "Description", "Email", "LinkWebsite", "Name", "Password", "Phone", "signature" },
                values: new object[,]
                {
                    { "39ae71a3-1f29-4c5e-a0e8-12e06b70f7b5", "456 Elm St, Othertown, USA", "Second Merchant Description", "sample2@gmail.com", "http://www.merchanttwo.com", "Merchant Two", "20ur290ur2ur8dd3d324r2r2r2", "098-765-4321", null },
                    { "e9b10673-5bf5-4c7c-be43-49740b7c6395", "123 Main St, Anytown, USA", "First Merchant Description", "sample@gmail.com", "http://www.merchantone.com", "Merchant One", "20ur290ur2ur824r2r2r2", "123-456-7890", null }
                });
        }
    }
}
