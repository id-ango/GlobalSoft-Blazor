﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using eSoft.Order.Data;

namespace Accounting.Migrations.DbContextOrderMigrations
{
    [DbContext(typeof(DbContextOrder))]
    partial class DbContextOrderModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.9")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("eSoft.Order.Model.PoTransD", b =>
                {
                    b.Property<int>("PoTransDId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("AcctSet")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Currency")
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("Discount")
                        .HasColumnType("decimal(18,4)");

                    b.Property<decimal>("Harga")
                        .HasColumnType("decimal(18,4)");

                    b.Property<string>("ItemCode")
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("JumDpp")
                        .HasColumnType("decimal(18,4)");

                    b.Property<decimal>("Jumlah")
                        .HasColumnType("decimal(18,4)");

                    b.Property<string>("Kode")
                        .HasMaxLength(2)
                        .HasColumnType("nvarchar(2)");

                    b.Property<decimal>("Kurs")
                        .HasColumnType("decimal(18,4)");

                    b.Property<string>("Lokasi")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("NamaItem")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("NoLpb")
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("Persen")
                        .HasColumnType("decimal(18,4)");

                    b.Property<int>("PoTransHId")
                        .HasColumnType("int");

                    b.Property<decimal>("Qty")
                        .HasColumnType("decimal(18,4)");

                    b.Property<decimal>("QtyBo")
                        .HasColumnType("decimal(18,4)");

                    b.Property<string>("Satuan")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("Tanggal")
                        .HasColumnType("datetime2");

                    b.Property<string>("Vendor")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("PoTransDId");

                    b.HasIndex("PoTransHId");

                    b.ToTable("PoTransDs");
                });

            modelBuilder.Entity("eSoft.Order.Model.PoTransH", b =>
                {
                    b.Property<int>("PoTransHId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Cek")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Currency")
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("DPayment")
                        .HasColumnType("decimal(18,4)");

                    b.Property<DateTime>("JthTempo")
                        .HasColumnType("datetime2");

                    b.Property<decimal>("Jumlah")
                        .HasColumnType("decimal(18,4)");

                    b.Property<string>("Keterangan")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Kode")
                        .HasMaxLength(2)
                        .HasColumnType("nvarchar(2)");

                    b.Property<decimal>("Kurs")
                        .HasColumnType("decimal(18,4)");

                    b.Property<string>("Lokasi")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("NamaVendor")
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("Nilai")
                        .HasColumnType("decimal(18,4)");

                    b.Property<string>("NoLpb")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("NoPrj")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("NoSJ")
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("Ongkos")
                        .HasColumnType("decimal(18,4)");

                    b.Property<bool>("Pajak")
                        .HasColumnType("bit");

                    b.Property<decimal>("Ppn")
                        .HasColumnType("decimal(18,4)");

                    b.Property<decimal>("PpnPersen")
                        .HasColumnType("decimal(18,4)");

                    b.Property<decimal>("QtyTerima")
                        .HasColumnType("decimal(18,4)");

                    b.Property<string>("Status")
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("Tagihan")
                        .HasColumnType("decimal(18,4)");

                    b.Property<DateTime>("Tanggal")
                        .HasColumnType("datetime2");

                    b.Property<decimal>("TotalQty")
                        .HasColumnType("decimal(18,4)");

                    b.Property<decimal>("TtlJumlah")
                        .HasColumnType("decimal(18,4)");

                    b.Property<string>("Vendor")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("PoTransHId");

                    b.ToTable("PoTransHs");
                });

            modelBuilder.Entity("eSoft.Order.Model.PoTransD", b =>
                {
                    b.HasOne("eSoft.Order.Model.PoTransH", "PoTransH")
                        .WithMany("PoTransDs")
                        .HasForeignKey("PoTransHId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("PoTransH");
                });

            modelBuilder.Entity("eSoft.Order.Model.PoTransH", b =>
                {
                    b.Navigation("PoTransDs");
                });
#pragma warning restore 612, 618
        }
    }
}
