using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TransactionsData.Migrations
{
    public partial class added2flds : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "DateSold",
                table: "ICCProductPins",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "TransactionId",
                table: "ICCProductPins",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DateSold",
                table: "ICCProductPins");

            migrationBuilder.DropColumn(
                name: "TransactionId",
                table: "ICCProductPins");
        }
    }
}
