using Microsoft.EntityFrameworkCore.Migrations;

namespace TransactionsData.Migrations
{
    public partial class add1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ICCProductPins_ICCProducts_ICCstkid",
                table: "ICCProductPins");

            migrationBuilder.DropForeignKey(
                name: "FK_ICCProductPins_ICCProductStockLevels_ICCProductStockLevelsid",
                table: "ICCProductPins");

            migrationBuilder.DropIndex(
                name: "IX_ICCProductPins_ICCProductStockLevelsid",
                table: "ICCProductPins");

            migrationBuilder.DropColumn(
                name: "ICCProductStockLevelsid",
                table: "ICCProductPins");

            migrationBuilder.AddForeignKey(
                name: "FK_ICCProductPins_ICCProductStockLevels_ICCstkid",
                table: "ICCProductPins",
                column: "ICCstkid",
                principalTable: "ICCProductStockLevels",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ICCProductPins_ICCProductStockLevels_ICCstkid",
                table: "ICCProductPins");

            migrationBuilder.AddColumn<int>(
                name: "ICCProductStockLevelsid",
                table: "ICCProductPins",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ICCProductPins_ICCProductStockLevelsid",
                table: "ICCProductPins",
                column: "ICCProductStockLevelsid");

            migrationBuilder.AddForeignKey(
                name: "FK_ICCProductPins_ICCProducts_ICCstkid",
                table: "ICCProductPins",
                column: "ICCstkid",
                principalTable: "ICCProducts",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ICCProductPins_ICCProductStockLevels_ICCProductStockLevelsid",
                table: "ICCProductPins",
                column: "ICCProductStockLevelsid",
                principalTable: "ICCProductStockLevels",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
