using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Accounting.Migrations.DbContextBankMigrations
{
    public partial class tambahbiayatransfer : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "Biaya",
                table: "CbTransfers",
                type: "decimal(18,4)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "KBiaya",
                table: "CbTransfers",
                type: "decimal(18,4)",
                nullable: false,
                defaultValue: 0m);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Biaya",
                table: "CbTransfers");

            migrationBuilder.DropColumn(
                name: "KBiaya",
                table: "CbTransfers");
        }
    }
}
