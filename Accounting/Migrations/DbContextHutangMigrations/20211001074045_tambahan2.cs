using Microsoft.EntityFrameworkCore.Migrations;

namespace Accounting.Migrations.DbContextHutangMigrations
{
    public partial class tambahan2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Currency",
                table: "ApTransHs",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Currency",
                table: "ApTransHs");
        }
    }
}
