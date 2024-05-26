using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace MerchantProfile.Api.Migrations
{
    /// <inheritdoc />
    public partial class initdb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Merchants",
                columns: table => new
                {
                    Id = table.Column<string>(type: "varchar(255)", nullable: false),
                    Name = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false),
                    Phone = table.Column<string>(type: "longtext", nullable: false),
                    Email = table.Column<string>(type: "longtext", nullable: false),
                    Description = table.Column<string>(type: "varchar(500)", maxLength: 500, nullable: true),
                    LinkWebsite = table.Column<string>(type: "longtext", nullable: true),
                    Address = table.Column<string>(type: "varchar(200)", maxLength: 200, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Merchants", x => x.Id);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.InsertData(
                table: "Merchants",
                columns: new[] { "Id", "Address", "Description", "Email", "LinkWebsite", "Name", "Phone" },
                values: new object[,]
                {
                    { "b51de648-7167-4360-ab8e-863bf3388ec0", "456 Elm St, Othertown, USA", "Second Merchant Description", "sample2@gmail.com", "http://www.merchanttwo.com", "Merchant Two", "098-765-4321" },
                    { "bda30663-e042-40a8-94a4-9c6f2a21764b", "123 Main St, Anytown, USA", "First Merchant Description", "sample@gmail.com", "http://www.merchantone.com", "Merchant One", "123-456-7890" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Merchants");
        }
    }
}
