using Microsoft.EntityFrameworkCore.Migrations;

namespace Accounting.Migrations.DbContextHutangMigrations
{
    public partial class tambahan : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "Kurs",
                table: "ApTransHs",
                type: "decimal(18,4)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "Nilai",
                table: "ApTransHs",
                type: "decimal(18,4)",
                nullable: false,
                defaultValue: 0m);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Kurs",
                table: "ApTransHs");

            migrationBuilder.DropColumn(
                name: "Nilai",
                table: "ApTransHs");
        }
    }
}
