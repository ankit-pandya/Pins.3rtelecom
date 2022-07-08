using Microsoft.EntityFrameworkCore.Migrations;

namespace TransactionsData.Migrations
{
    public partial class FA2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductDenominations_tblproducts_productsid",
                table: "ProductDenominations");

            migrationBuilder.DropForeignKey(
                name: "FK_tblproductpins_ProductDenominations_productDenominationsid",
                table: "tblproductpins");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ProductDenominations",
                table: "ProductDenominations");

            migrationBuilder.RenameTable(
                name: "ProductDenominations",
                newName: "tblproductdenoms");

            migrationBuilder.RenameIndex(
                name: "IX_ProductDenominations_productsid",
                table: "tblproductdenoms",
                newName: "IX_tblproductdenoms_productsid");

            migrationBuilder.AddPrimaryKey(
                name: "PK_tblproductdenoms",
                table: "tblproductdenoms",
                column: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_tblproductdenoms_tblproducts_productsid",
                table: "tblproductdenoms",
                column: "productsid",
                principalTable: "tblproducts",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_tblproductpins_tblproductdenoms_productDenominationsid",
                table: "tblproductpins",
                column: "productDenominationsid",
                principalTable: "tblproductdenoms",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_tblproductdenoms_tblproducts_productsid",
                table: "tblproductdenoms");

            migrationBuilder.DropForeignKey(
                name: "FK_tblproductpins_tblproductdenoms_productDenominationsid",
                table: "tblproductpins");

            migrationBuilder.DropPrimaryKey(
                name: "PK_tblproductdenoms",
                table: "tblproductdenoms");

            migrationBuilder.RenameTable(
                name: "tblproductdenoms",
                newName: "ProductDenominations");

            migrationBuilder.RenameIndex(
                name: "IX_tblproductdenoms_productsid",
                table: "ProductDenominations",
                newName: "IX_ProductDenominations_productsid");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProductDenominations",
                table: "ProductDenominations",
                column: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductDenominations_tblproducts_productsid",
                table: "ProductDenominations",
                column: "productsid",
                principalTable: "tblproducts",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_tblproductpins_ProductDenominations_productDenominationsid",
                table: "tblproductpins",
                column: "productDenominationsid",
                principalTable: "ProductDenominations",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
