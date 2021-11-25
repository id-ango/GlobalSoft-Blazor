using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Accounting.Migrations.DbContextBankMigrations
{
    public partial class cokode : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CoKode",
                table: "CbBanks",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CoKode",
                table: "CbBanks");
        }
    }
}
