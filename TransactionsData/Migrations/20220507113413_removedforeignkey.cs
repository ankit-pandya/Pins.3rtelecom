using Microsoft.EntityFrameworkCore.Migrations;

namespace TransactionsData.Migrations
{
    public partial class removedforeignkey : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MerchantHours_Merchants_merchantID",
                table: "MerchantHours");

            migrationBuilder.DropIndex(
                name: "IX_MerchantHours_merchantID",
                table: "MerchantHours");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_MerchantHours_merchantID",
                table: "MerchantHours",
                column: "merchantID");

            migrationBuilder.AddForeignKey(
                name: "FK_MerchantHours_Merchants_merchantID",
                table: "MerchantHours",
                column: "merchantID",
                principalTable: "Merchants",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
