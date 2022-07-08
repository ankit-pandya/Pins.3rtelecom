using Microsoft.EntityFrameworkCore.Migrations;

namespace TransactionsData.Migrations
{
    public partial class mechanthuraded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "MerchantHours",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MonOH = table.Column<int>(type: "int", nullable: false),
                    MonOM = table.Column<int>(type: "int", nullable: false),
                    MonCH = table.Column<int>(type: "int", nullable: false),
                    MonCM = table.Column<int>(type: "int", nullable: false),
                    TueOH = table.Column<int>(type: "int", nullable: false),
                    TueOM = table.Column<int>(type: "int", nullable: false),
                    TueCH = table.Column<int>(type: "int", nullable: false),
                    TueCM = table.Column<int>(type: "int", nullable: false),
                    WedOH = table.Column<int>(type: "int", nullable: false),
                    WedOM = table.Column<int>(type: "int", nullable: false),
                    WedCH = table.Column<int>(type: "int", nullable: false),
                    WedCM = table.Column<int>(type: "int", nullable: false),
                    ThuOH = table.Column<int>(type: "int", nullable: false),
                    ThuOM = table.Column<int>(type: "int", nullable: false),
                    ThuCH = table.Column<int>(type: "int", nullable: false),
                    ThuCM = table.Column<int>(type: "int", nullable: false),
                    FriOH = table.Column<int>(type: "int", nullable: false),
                    FriOM = table.Column<int>(type: "int", nullable: false),
                    FriCH = table.Column<int>(type: "int", nullable: false),
                    FriCM = table.Column<int>(type: "int", nullable: false),
                    SatOH = table.Column<int>(type: "int", nullable: false),
                    SatOM = table.Column<int>(type: "int", nullable: false),
                    SatCH = table.Column<int>(type: "int", nullable: false),
                    SatCM = table.Column<int>(type: "int", nullable: false),
                    SunOH = table.Column<int>(type: "int", nullable: false),
                    SunOM = table.Column<int>(type: "int", nullable: false),
                    SunCH = table.Column<int>(type: "int", nullable: false),
                    SunCM = table.Column<int>(type: "int", nullable: false),
                    merchantID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MerchantHours", x => x.id);
                    table.ForeignKey(
                        name: "FK_MerchantHours_Merchants_merchantID",
                        column: x => x.merchantID,
                        principalTable: "Merchants",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_MerchantHours_merchantID",
                table: "MerchantHours",
                column: "merchantID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MerchantHours");
        }
    }
}
