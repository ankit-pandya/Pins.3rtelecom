using Microsoft.EntityFrameworkCore.Migrations;

namespace TransactionsData.Migrations
{
    public partial class addedtables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "tblproviders",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    providerCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    providerName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    status = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblproviders", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "tblproducts",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    productCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    productName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    status = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    topupLevel = table.Column<int>(type: "int", nullable: false),
                    information = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    providerCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    providertblid = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblproducts", x => x.id);
                    table.ForeignKey(
                        name: "FK_tblproducts_tblproviders_providertblid",
                        column: x => x.providertblid,
                        principalTable: "tblproviders",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_tblproducts_providertblid",
                table: "tblproducts",
                column: "providertblid");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tblproducts");

            migrationBuilder.DropTable(
                name: "tblproviders");
        }
    }
}
