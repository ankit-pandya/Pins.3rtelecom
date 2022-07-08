using Microsoft.EntityFrameworkCore.Migrations;

namespace TransactionsData.Migrations
{
    public partial class MTProducts : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "tblMTProducts",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    productCode3r = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    productCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    productName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    status = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    value = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    information = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    provider3r = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    product3r = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblMTProducts", x => x.id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tblMTProducts");
        }
    }
}
