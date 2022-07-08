using Microsoft.EntityFrameworkCore.Migrations;

namespace TransactionsData.Migrations
{
    public partial class hourstomerchanttable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MerchantHours");

            migrationBuilder.AddColumn<string>(
                name: "FriCH",
                table: "Merchants",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FriCM",
                table: "Merchants",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FriOH",
                table: "Merchants",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FriOM",
                table: "Merchants",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "MonCH",
                table: "Merchants",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "MonCM",
                table: "Merchants",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "MonOH",
                table: "Merchants",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "MonOM",
                table: "Merchants",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SatCH",
                table: "Merchants",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SatCM",
                table: "Merchants",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SatOH",
                table: "Merchants",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SatOM",
                table: "Merchants",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SunCH",
                table: "Merchants",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SunCM",
                table: "Merchants",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SunOH",
                table: "Merchants",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SunOM",
                table: "Merchants",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ThuCH",
                table: "Merchants",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ThuCM",
                table: "Merchants",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ThuOH",
                table: "Merchants",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ThuOM",
                table: "Merchants",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TueCH",
                table: "Merchants",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TueCM",
                table: "Merchants",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TueOH",
                table: "Merchants",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TueOM",
                table: "Merchants",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "WedCH",
                table: "Merchants",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "WedCM",
                table: "Merchants",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "WedOH",
                table: "Merchants",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "WedOM",
                table: "Merchants",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FriCH",
                table: "Merchants");

            migrationBuilder.DropColumn(
                name: "FriCM",
                table: "Merchants");

            migrationBuilder.DropColumn(
                name: "FriOH",
                table: "Merchants");

            migrationBuilder.DropColumn(
                name: "FriOM",
                table: "Merchants");

            migrationBuilder.DropColumn(
                name: "MonCH",
                table: "Merchants");

            migrationBuilder.DropColumn(
                name: "MonCM",
                table: "Merchants");

            migrationBuilder.DropColumn(
                name: "MonOH",
                table: "Merchants");

            migrationBuilder.DropColumn(
                name: "MonOM",
                table: "Merchants");

            migrationBuilder.DropColumn(
                name: "SatCH",
                table: "Merchants");

            migrationBuilder.DropColumn(
                name: "SatCM",
                table: "Merchants");

            migrationBuilder.DropColumn(
                name: "SatOH",
                table: "Merchants");

            migrationBuilder.DropColumn(
                name: "SatOM",
                table: "Merchants");

            migrationBuilder.DropColumn(
                name: "SunCH",
                table: "Merchants");

            migrationBuilder.DropColumn(
                name: "SunCM",
                table: "Merchants");

            migrationBuilder.DropColumn(
                name: "SunOH",
                table: "Merchants");

            migrationBuilder.DropColumn(
                name: "SunOM",
                table: "Merchants");

            migrationBuilder.DropColumn(
                name: "ThuCH",
                table: "Merchants");

            migrationBuilder.DropColumn(
                name: "ThuCM",
                table: "Merchants");

            migrationBuilder.DropColumn(
                name: "ThuOH",
                table: "Merchants");

            migrationBuilder.DropColumn(
                name: "ThuOM",
                table: "Merchants");

            migrationBuilder.DropColumn(
                name: "TueCH",
                table: "Merchants");

            migrationBuilder.DropColumn(
                name: "TueCM",
                table: "Merchants");

            migrationBuilder.DropColumn(
                name: "TueOH",
                table: "Merchants");

            migrationBuilder.DropColumn(
                name: "TueOM",
                table: "Merchants");

            migrationBuilder.DropColumn(
                name: "WedCH",
                table: "Merchants");

            migrationBuilder.DropColumn(
                name: "WedCM",
                table: "Merchants");

            migrationBuilder.DropColumn(
                name: "WedOH",
                table: "Merchants");

            migrationBuilder.DropColumn(
                name: "WedOM",
                table: "Merchants");

            migrationBuilder.CreateTable(
                name: "MerchantHours",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FriCH = table.Column<int>(type: "int", nullable: false),
                    FriCM = table.Column<int>(type: "int", nullable: false),
                    FriOH = table.Column<int>(type: "int", nullable: false),
                    FriOM = table.Column<int>(type: "int", nullable: false),
                    MonCH = table.Column<int>(type: "int", nullable: false),
                    MonCM = table.Column<int>(type: "int", nullable: false),
                    MonOH = table.Column<int>(type: "int", nullable: false),
                    MonOM = table.Column<int>(type: "int", nullable: false),
                    SatCH = table.Column<int>(type: "int", nullable: false),
                    SatCM = table.Column<int>(type: "int", nullable: false),
                    SatOH = table.Column<int>(type: "int", nullable: false),
                    SatOM = table.Column<int>(type: "int", nullable: false),
                    SunCH = table.Column<int>(type: "int", nullable: false),
                    SunCM = table.Column<int>(type: "int", nullable: false),
                    SunOH = table.Column<int>(type: "int", nullable: false),
                    SunOM = table.Column<int>(type: "int", nullable: false),
                    ThuCH = table.Column<int>(type: "int", nullable: false),
                    ThuCM = table.Column<int>(type: "int", nullable: false),
                    ThuOH = table.Column<int>(type: "int", nullable: false),
                    ThuOM = table.Column<int>(type: "int", nullable: false),
                    TueCH = table.Column<int>(type: "int", nullable: false),
                    TueCM = table.Column<int>(type: "int", nullable: false),
                    TueOH = table.Column<int>(type: "int", nullable: false),
                    TueOM = table.Column<int>(type: "int", nullable: false),
                    WedCH = table.Column<int>(type: "int", nullable: false),
                    WedCM = table.Column<int>(type: "int", nullable: false),
                    WedOH = table.Column<int>(type: "int", nullable: false),
                    WedOM = table.Column<int>(type: "int", nullable: false),
                    merchantID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MerchantHours", x => x.id);
                });
        }
    }
}
