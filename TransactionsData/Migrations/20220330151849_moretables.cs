using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TransactionsData.Migrations
{
    public partial class moretables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "tblproductdenoms",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    freephoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    generalNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    londonNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    helplineNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    followOnCall = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    expiryPeriod = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    denomination = table.Column<double>(type: "float", nullable: false),
                    dateCreated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    numberAdded = table.Column<int>(type: "int", nullable: false),
                    numberRemaining = table.Column<int>(type: "int", nullable: false),
                    productCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    productsid = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblproductdenoms", x => x.id);
                    table.ForeignKey(
                        name: "FK_tblproductdenoms_tblproducts_productsid",
                        column: x => x.productsid,
                        principalTable: "tblproducts",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "tblproductpins",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    pin = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    serial = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    dateSold = table.Column<DateTime>(type: "datetime2", nullable: false),
                    transactionId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    productDenominationsid = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblproductpins", x => x.id);
                    table.ForeignKey(
                        name: "FK_tblproductpins_tblproductdenoms_productDenominationsid",
                        column: x => x.productDenominationsid,
                        principalTable: "tblproductdenoms",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_tblproductdenoms_productsid",
                table: "tblproductdenoms",
                column: "productsid");

            migrationBuilder.CreateIndex(
                name: "IX_tblproductpins_productDenominationsid",
                table: "tblproductpins",
                column: "productDenominationsid");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tblproductpins");

            migrationBuilder.DropTable(
                name: "tblproductdenoms");
        }
    }
}
