﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using eSoft.Piutang.Data;

#nullable disable

namespace Accounting.Migrations.DbContextPiutangMigrations
{
    [DbContext(typeof(DbContextPiutang))]
    [Migration("20211130074416_initialcreate")]
    partial class initialcreate
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("eSoft.Piutang.Model.ArAcct", b =>
                {
                    b.Property<int>("ArAcctId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ArAcctId"), 1L, 1);

                    b.Property<string>("Acct1")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Acct2")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Acct3")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Acct4")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Acct5")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Acct6")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("AcctSet")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ArAcctId");

                    b.ToTable("ArAccts");
                });

            modelBuilder.Entity("eSoft.Piutang.Model.ArCust", b =>
                {
                    b.Property<int>("ArCustId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ArCustId"), 1L, 1);

                    b.Property<string>("AcctPjk")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("AcctSet")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Alamat")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("AlmtKrm")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("AlmtNPWP")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Customer")
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("Disc1")
                        .HasColumnType("decimal(18,4)");

                    b.Property<decimal>("Disc2")
                        .HasColumnType("decimal(18,4)");

                    b.Property<string>("Expedisi")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Fax")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Golongan")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Kontak")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Kota")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("KotaKrm")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("LstOrder")
                        .HasColumnType("datetime2");

                    b.Property<string>("NPWP_Cust")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("NamaCust")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("NamaLengkap")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("NonPPN")
                        .HasColumnType("bit");

                    b.Property<bool>("Pajak")
                        .HasColumnType("bit");

                    b.Property<decimal>("Piutang")
                        .HasColumnType("decimal(18,4)");

                    b.Property<string>("ProvKirim")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Provinsi")
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("SldAwal")
                        .HasColumnType("decimal(18,4)");

                    b.Property<string>("Telpon")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Termin")
                        .HasColumnType("int");

                    b.Property<DateTime?>("TglMasuk")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("TglPost")
                        .HasColumnType("datetime2");

                    b.HasKey("ArCustId");

                    b.ToTable("ArCusts");
                });

            modelBuilder.Entity("eSoft.Piutang.Model.ArDist", b =>
                {
                    b.Property<int>("ArDistId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ArDistId"), 1L, 1);

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Dist1")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("DistCode")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ArDistId");

                    b.ToTable("ArDists");
                });

            modelBuilder.Entity("eSoft.Piutang.Model.ArPiutng", b =>
                {
                    b.Property<int>("ArPiutngId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ArPiutngId"), 1L, 1);

                    b.Property<decimal>("Bayar")
                        .HasColumnType("decimal(18,4)");

                    b.Property<string>("Customer")
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("Discount")
                        .HasColumnType("decimal(18,4)");

                    b.Property<string>("Dokumen")
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("Dpp")
                        .HasColumnType("decimal(18,4)");

                    b.Property<DateTime?>("DueDate")
                        .HasColumnType("datetime2");

                    b.Property<decimal>("Jumlah")
                        .HasColumnType("decimal(18,4)");

                    b.Property<string>("Keterangan")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Kode")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("KodeTran")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LPB")
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("PPh")
                        .HasColumnType("decimal(18,4)");

                    b.Property<decimal>("PPn")
                        .HasColumnType("decimal(18,4)");

                    b.Property<bool>("Pajak")
                        .HasColumnType("bit");

                    b.Property<decimal>("Sisa")
                        .HasColumnType("decimal(18,4)");

                    b.Property<decimal>("SldBayar")
                        .HasColumnType("decimal(18,4)");

                    b.Property<decimal>("SldDisc")
                        .HasColumnType("decimal(18,4)");

                    b.Property<decimal>("SldSisa")
                        .HasColumnType("decimal(18,4)");

                    b.Property<decimal>("SldUnpl")
                        .HasColumnType("decimal(18,4)");

                    b.Property<DateTime>("Tanggal")
                        .HasColumnType("datetime2");

                    b.Property<decimal>("UnApplied")
                        .HasColumnType("decimal(18,4)");

                    b.HasKey("ArPiutngId");

                    b.ToTable("ArPiutngs");
                });

            modelBuilder.Entity("eSoft.Piutang.Model.ArTransD", b =>
                {
                    b.Property<int>("ArTransDId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ArTransDId"), 1L, 1);

                    b.Property<int>("ArTransHId")
                        .HasColumnType("int");

                    b.Property<decimal>("Bayar")
                        .HasColumnType("decimal(18,4)");

                    b.Property<string>("Bukti")
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("Discount")
                        .HasColumnType("decimal(18,4)");

                    b.Property<string>("DistCode")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("DueDate")
                        .HasColumnType("datetime2");

                    b.Property<decimal>("Jumlah")
                        .HasColumnType("decimal(18,4)");

                    b.Property<string>("Keterangan")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Kode")
                        .HasMaxLength(2)
                        .HasColumnType("nvarchar(2)");

                    b.Property<string>("KodeTran")
                        .HasMaxLength(2)
                        .HasColumnType("nvarchar(2)");

                    b.Property<string>("Lpb")
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("Sisa")
                        .HasColumnType("decimal(18,4)");

                    b.Property<DateTime>("Tanggal")
                        .HasColumnType("datetime2");

                    b.HasKey("ArTransDId");

                    b.HasIndex("ArTransHId");

                    b.ToTable("ArTransDs");
                });

            modelBuilder.Entity("eSoft.Piutang.Model.ArTransH", b =>
                {
                    b.Property<int>("ArTransHId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ArTransHId"), 1L, 1);

                    b.Property<string>("AcctSet")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("ArCustId")
                        .HasColumnType("int");

                    b.Property<decimal>("Bruto")
                        .HasColumnType("decimal(18,4)");

                    b.Property<string>("Bukti")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Customer")
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("Discount")
                        .HasColumnType("decimal(18,4)");

                    b.Property<DateTime?>("JthTempo")
                        .HasColumnType("datetime2");

                    b.Property<decimal>("JumPPh")
                        .HasColumnType("decimal(18,4)");

                    b.Property<decimal>("JumPPn")
                        .HasColumnType("decimal(18,4)");

                    b.Property<decimal>("Jumlah")
                        .HasColumnType("decimal(18,4)");

                    b.Property<string>("KdBank")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Keterangan")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Kode")
                        .HasMaxLength(2)
                        .HasColumnType("nvarchar(2)");

                    b.Property<string>("NamaCust")
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("Netto")
                        .HasColumnType("decimal(18,4)");

                    b.Property<string>("NoFaktur")
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("PPh")
                        .HasColumnType("decimal(18,4)");

                    b.Property<decimal>("PPn")
                        .HasColumnType("decimal(18,4)");

                    b.Property<bool>("Pajak")
                        .HasColumnType("bit");

                    b.Property<decimal>("Piutang")
                        .HasColumnType("decimal(18,4)");

                    b.Property<DateTime>("Tanggal")
                        .HasColumnType("datetime2");

                    b.Property<decimal>("Unapplied")
                        .HasColumnType("decimal(18,4)");

                    b.HasKey("ArTransHId");

                    b.ToTable("ArTransHs");
                });

            modelBuilder.Entity("eSoft.Piutang.Model.ArTransD", b =>
                {
                    b.HasOne("eSoft.Piutang.Model.ArTransH", "ArTransH")
                        .WithMany("ArTransDs")
                        .HasForeignKey("ArTransHId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("ArTransH");
                });

            modelBuilder.Entity("eSoft.Piutang.Model.ArTransH", b =>
                {
                    b.Navigation("ArTransDs");
                });
#pragma warning restore 612, 618
        }
    }
}