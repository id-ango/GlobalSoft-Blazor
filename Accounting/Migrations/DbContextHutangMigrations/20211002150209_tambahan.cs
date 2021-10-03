using Microsoft.EntityFrameworkCore.Migrations;

namespace Accounting.Migrations.DbContextHutangMigrations
{
    public partial class tambahan : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Currency",
                table: "ApHutangs",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "Kurs",
                table: "ApHutangs",
                type: "decimal(18,4)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "Nilai",
                table: "ApHutangs",
                type: "decimal(18,4)",
                nullable: false,
                defaultValue: 0m);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Currency",
                table: "ApHutangs");

            migrationBuilder.DropColumn(
                name: "Kurs",
                table: "ApHutangs");

            migrationBuilder.DropColumn(
                name: "Nilai",
                table: "ApHutangs");
        }
    }
}
