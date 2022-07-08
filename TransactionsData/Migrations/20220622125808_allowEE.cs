using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TransactionsData.Migrations
{
    public partial class allowEE : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ICCProductPins");

            migrationBuilder.DropTable(
                name: "ICCProducts");

            migrationBuilder.DropTable(
                name: "ICCProviders");

            migrationBuilder.DropTable(
                name: "ICCProductStockLevels");

            migrationBuilder.AddColumn<bool>(
                name: "allowEE",
                table: "Merchants",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "allowEE",
                table: "Merchants");

            migrationBuilder.CreateTable(
                name: "ICCProducts",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Information = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ProductCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ProductName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SupplierID = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TopupLevel = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ICCProducts", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "ICCProductStockLevels",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ExpiryPeriod = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FollowOnCall = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FreephoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    GeneralNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    HelplineNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LondonNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ProductCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ProviderCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    dateCreated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    numberAdded = table.Column<int>(type: "int", nullable: false),
                    numberRemaining = table.Column<int>(type: "int", nullable: false),
                    value = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ICCProductStockLevels", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "ICCProviders",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SupplierID = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SupplierName = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ICCProviders", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "ICCProductPins",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DateSold = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ICCstkid = table.Column<int>(type: "int", nullable: false),
                    Pin = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Serial = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TransactionId = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ICCProductPins", x => x.id);
                    table.ForeignKey(
                        name: "FK_ICCProductPins_ICCProductStockLevels_ICCstkid",
                        column: x => x.ICCstkid,
                        principalTable: "ICCProductStockLevels",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ICCProductPins_ICCstkid",
                table: "ICCProductPins",
                column: "ICCstkid");
        }
    }
}
