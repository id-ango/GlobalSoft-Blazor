using Microsoft.EntityFrameworkCore.Migrations;

namespace Accounting.Migrations.DbContextBankMigrations
{
    public partial class transferbank : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Kurs2",
                table: "CbTransfers",
                type: "nvarchar(3)",
                maxLength: 3,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Kurs2",
                table: "CbTransfers");
        }
    }
}
