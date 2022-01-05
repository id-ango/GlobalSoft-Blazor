﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using eSoft.Pembelian.Data;

#nullable disable

namespace Accounting.Migrations.DbContextBeliMigrations
{
    [DbContext(typeof(DbContextBeli))]
    [Migration("20211231050614_initialbeli")]
    partial class initialbeli
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.1")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("eSoft.Pembelian.Model.IrTransD", b =>
                {
                    b.Property<int>("IrTransDId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IrTransDId"), 1L, 1);

                    b.Property<string>("AcctSet")
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("Discount")
                        .HasColumnType("decimal(18,4)");

                    b.Property<decimal>("Harga")
                        .HasColumnType("decimal(18,4)");

                    b.Property<int>("IrTransHId")
                        .HasColumnType("int");

                    b.Property<string>("ItemCode")
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("JumDpp")
                        .HasColumnType("decimal(18,4)");

                    b.Property<decimal>("Jumlah")
                        .HasColumnType("decimal(18,4)");

                    b.Property<string>("Kode")
                        .HasMaxLength(2)
                        .HasColumnType("nvarchar(2)");

                    b.Property<string>("Lokasi")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("NamaItem")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("NoLpb")
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("Persen")
                        .HasColumnType("decimal(18,4)");

                    b.Property<decimal>("Qty")
                        .HasColumnType("decimal(18,4)");

                    b.Property<decimal>("QtyBo")
                        .HasColumnType("decimal(18,4)");

                    b.Property<string>("Satuan")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Supplier")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("Tanggal")
                        .HasColumnType("datetime2");

                    b.HasKey("IrTransDId");

                    b.HasIndex("IrTransHId");

                    b.ToTable("IrTransDs");
                });

            modelBuilder.Entity("eSoft.Pembelian.Model.IrTransH", b =>
                {
                    b.Property<int>("IrTransHId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IrTransHId"), 1L, 1);

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

                    b.Property<string>("NamaSup")
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

                    b.Property<string>("Supplier")
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("Tagihan")
                        .HasColumnType("decimal(18,4)");

                    b.Property<DateTime>("Tanggal")
                        .HasColumnType("datetime2");

                    b.Property<decimal>("TotalQty")
                        .HasColumnType("decimal(18,4)");

                    b.Property<decimal>("TtlJumlah")
                        .HasColumnType("decimal(18,4)");

                    b.HasKey("IrTransHId");

                    b.ToTable("IrTransHs");
                });

            modelBuilder.Entity("eSoft.Pembelian.Model.IrTransD", b =>
                {
                    b.HasOne("eSoft.Pembelian.Model.IrTransH", "IrTransH")
                        .WithMany("IrTransDs")
                        .HasForeignKey("IrTransHId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("IrTransH");
                });

            modelBuilder.Entity("eSoft.Pembelian.Model.IrTransH", b =>
                {
                    b.Navigation("IrTransDs");
                });
#pragma warning restore 612, 618
        }
    }
}