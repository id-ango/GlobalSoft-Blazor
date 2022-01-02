using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Accounting.Migrations.DbContextJualMigrations
{
    public partial class initialjual : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "OeTransHs",
                columns: table => new
                {
                    OeTransHId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Kode = table.Column<string>(type: "nvarchar(2)", maxLength: 2, nullable: true),
                    NoLpb = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NoSJ = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NoPrj = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Lokasi = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Tanggal = table.Column<DateTime>(type: "datetime2", nullable: false),
                    JthTempo = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TtlJumlah = table.Column<decimal>(type: "decimal(18,4)", nullable: false),
                    DPayment = table.Column<decimal>(type: "decimal(18,4)", nullable: false),
                    Ongkos = table.Column<decimal>(type: "decimal(18,4)", nullable: false),
                    PpnPersen = table.Column<decimal>(type: "decimal(18,4)", nullable: false),
                    Ppn = table.Column<decimal>(type: "decimal(18,4)", nullable: false),
                    Jumlah = table.Column<decimal>(type: "decimal(18,4)", nullable: false),
                    Tagihan = table.Column<decimal>(type: "decimal(18,4)", nullable: false),
                    TotalQty = table.Column<decimal>(type: "decimal(18,4)", nullable: false),
                    QtyTerima = table.Column<decimal>(type: "decimal(18,4)", nullable: false),
                    Customer = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NamaCust = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Keterangan = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AlamatKirim = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Cek = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Pajak = table.Column<bool>(type: "bit", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OeTransHs", x => x.OeTransHId);
                });

            migrationBuilder.CreateTable(
                name: "OeTransDs",
                columns: table => new
                {
                    OeTransDId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Kode = table.Column<string>(type: "nvarchar(2)", maxLength: 2, nullable: true),
                    NoLpb = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Tanggal = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ItemCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NamaItem = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Satuan = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Lokasi = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Harga = table.Column<decimal>(type: "decimal(18,4)", nullable: false),
                    Persen = table.Column<decimal>(type: "decimal(18,4)", nullable: false),
                    Discount = table.Column<decimal>(type: "decimal(18,4)", nullable: false),
                    Qty = table.Column<decimal>(type: "decimal(18,4)", nullable: false),
                    QtyBo = table.Column<decimal>(type: "decimal(18,4)", nullable: false),
                    Cost = table.Column<decimal>(type: "decimal(18,4)", nullable: false),
                    HrgCost = table.Column<decimal>(type: "decimal(18,4)", nullable: false),
                    JumDpp = table.Column<decimal>(type: "decimal(18,4)", nullable: false),
                    Jumlah = table.Column<decimal>(type: "decimal(18,4)", nullable: false),
                    Customer = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AcctSet = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OeTransHId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OeTransDs", x => x.OeTransDId);
                    table.ForeignKey(
                        name: "FK_OeTransDs_OeTransHs_OeTransHId",
                        column: x => x.OeTransHId,
                        principalTable: "OeTransHs",
                        principalColumn: "OeTransHId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_OeTransDs_OeTransHId",
                table: "OeTransDs",
                column: "OeTransHId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OeTransDs");

            migrationBuilder.DropTable(
                name: "OeTransHs");
        }
    }
}
