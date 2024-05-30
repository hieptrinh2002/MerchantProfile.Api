using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace MerchantProfile.Api.Migrations
{
    /// <inheritdoc />
    public partial class initDb : Migration
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
                    Address = table.Column<string>(type: "varchar(200)", maxLength: 200, nullable: false),
                    Password = table.Column<string>(type: "longtext", nullable: false),
                    signature = table.Column<string>(type: "longtext", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Merchants", x => x.Id);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.InsertData(
                table: "Merchants",
                columns: new[] { "Id", "Address", "Description", "Email", "LinkWebsite", "Name", "Password", "Phone", "signature" },
                values: new object[,]
                {
                    { "39ae71a3-1f29-4c5e-a0e8-12e06b70f7b5", "456 Elm St, Othertown, USA", "Second Merchant Description", "sample2@gmail.com", "http://www.merchanttwo.com", "Merchant Two", "20ur290ur2ur8dd3d324r2r2r2", "098-765-4321", null },
                    { "e9b10673-5bf5-4c7c-be43-49740b7c6395", "123 Main St, Anytown, USA", "First Merchant Description", "sample@gmail.com", "http://www.merchantone.com", "Merchant One", "20ur290ur2ur824r2r2r2", "123-456-7890", null }
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
