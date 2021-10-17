using Microsoft.EntityFrameworkCore.Migrations;

namespace Accounting.Migrations.DbContextBeliMigrations
{
    public partial class kurs : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Currency",
                table: "IrTransHs",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "Kurs",
                table: "IrTransHs",
                type: "decimal(18,4)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "Nilai",
                table: "IrTransHs",
                type: "decimal(18,4)",
                nullable: false,
                defaultValue: 0m);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Currency",
                table: "IrTransHs");

            migrationBuilder.DropColumn(
                name: "Kurs",
                table: "IrTransHs");

            migrationBuilder.DropColumn(
                name: "Nilai",
                table: "IrTransHs");
        }
    }
}
