﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using eSoft.CashBank.Data;

namespace Accounting.Migrations
{
    [DbContext(typeof(DbContextBank))]
    partial class DbContextBankModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.4")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("eSoft.CashBank.Model.CbBank", b =>
                {
                    b.Property<int>("CbBankId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Acctset")
                        .HasMaxLength(7)
                        .HasColumnType("nvarchar(7)");

                    b.Property<DateTime>("ClrDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("GrpBank")
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("KSaldo")
                        .HasColumnType("decimal(18,4)");

                    b.Property<decimal>("KSldAwal")
                        .HasColumnType("decimal(18,4)");

                    b.Property<string>("KodeBank")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Kurs")
                        .HasMaxLength(3)
                        .HasColumnType("nvarchar(3)");

                    b.Property<string>("NmBank")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<bool>("NonPpn")
                        .HasColumnType("bit");

                    b.Property<bool>("Pajak")
                        .HasColumnType("bit");

                    b.Property<decimal>("Saldo")
                        .HasColumnType("decimal(18,4)");

                    b.Property<decimal>("SldAwal")
                        .HasColumnType("decimal(18,4)");

                    b.Property<string>("Status")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("CbBankId");

                    b.HasIndex("KodeBank")
                        .IsUnique()
                        .HasFilter("[KodeBank] IS NOT NULL");

                    b.ToTable("Banks");
                });

            modelBuilder.Entity("eSoft.CashBank.Model.CbGrp", b =>
                {
                    b.Property<int>("CbGrpId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Grp")
                        .HasMaxLength(3)
                        .HasColumnType("nvarchar(3)");

                    b.Property<string>("NamaGrp")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("CbGrpId");

                    b.ToTable("CbGrps");
                });

            modelBuilder.Entity("eSoft.CashBank.Model.CbSrcCode", b =>
                {
                    b.Property<int>("CbSrcCodeId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("GlAcct")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Grp")
                        .HasMaxLength(3)
                        .HasColumnType("nvarchar(3)");

                    b.Property<string>("NamaSrc")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("SrcCode")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("CbSrcCodeId");

                    b.ToTable("CbSrcCodes");
                });

            modelBuilder.Entity("eSoft.CashBank.Model.CbTransD", b =>
                {
                    b.Property<int>("CbTransDId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<decimal>("Bayar")
                        .HasColumnType("decimal(18,4)");

                    b.Property<int>("CbTransHId")
                        .HasColumnType("int");

                    b.Property<string>("GlAcct")
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("Jumlah")
                        .HasColumnType("decimal(18,4)");

                    b.Property<decimal>("KBayar")
                        .HasColumnType("decimal(18,4)");

                    b.Property<decimal>("KJumlah")
                        .HasColumnType("decimal(18,4)");

                    b.Property<decimal>("KTerima")
                        .HasColumnType("decimal(18,4)");

                    b.Property<decimal>("KValue")
                        .HasColumnType("decimal(18,4)");

                    b.Property<string>("Keterangan")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Kurs")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("NoPrj")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("SrcCode")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("SrcCodeId")
                        .HasColumnType("int");

                    b.Property<decimal>("Terima")
                        .HasColumnType("decimal(18,4)");

                    b.HasKey("CbTransDId");

                    b.HasIndex("CbTransHId");

                    b.ToTable("CbTransDs");
                });

            modelBuilder.Entity("eSoft.CashBank.Model.CbTransH", b =>
                {
                    b.Property<int>("CbTransHId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Acctset")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("Cek")
                        .HasColumnType("bit");

                    b.Property<string>("Customer")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("DocNo")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Giro")
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("KSaldo")
                        .HasColumnType("decimal(18,4)");

                    b.Property<string>("Keterangan")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("KodeBank")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Kurs")
                        .HasMaxLength(3)
                        .HasColumnType("nvarchar(3)");

                    b.Property<bool>("NonPPN")
                        .HasColumnType("bit");

                    b.Property<bool>("Pajak")
                        .HasColumnType("bit");

                    b.Property<string>("Refno")
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("Saldo")
                        .HasColumnType("decimal(18,4)");

                    b.Property<string>("Supplier")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("Tanggal")
                        .HasColumnType("datetime2");

                    b.HasKey("CbTransHId");

                    b.ToTable("CbTransHs");
                });

            modelBuilder.Entity("eSoft.CashBank.Model.CbTransfer", b =>
                {
                    b.Property<int>("CbTransferId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Acctset")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("Cek")
                        .HasColumnType("bit");

                    b.Property<string>("DocNo")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Giro")
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("KSaldo")
                        .HasColumnType("decimal(18,4)");

                    b.Property<decimal>("KValue")
                        .HasColumnType("decimal(18,4)");

                    b.Property<string>("Keterangan")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("KodeBank1")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("KodeBank2")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Kurs")
                        .HasMaxLength(3)
                        .HasColumnType("nvarchar(3)");

                    b.Property<bool>("NonPPN")
                        .HasColumnType("bit");

                    b.Property<bool>("Pajak")
                        .HasColumnType("bit");

                    b.Property<string>("Refno")
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("Saldo")
                        .HasColumnType("decimal(18,4)");

                    b.Property<DateTime>("Tanggal")
                        .HasColumnType("datetime2");

                    b.HasKey("CbTransferId");

                    b.ToTable("CbTransfers");
                });

            modelBuilder.Entity("eSoft.CashBank.Model.CbTransD", b =>
                {
                    b.HasOne("eSoft.CashBank.Model.CbTransH", "CbTransH")
                        .WithMany("CbTransDs")
                        .HasForeignKey("CbTransHId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("CbTransH");
                });

            modelBuilder.Entity("eSoft.CashBank.Model.CbTransH", b =>
                {
                    b.Navigation("CbTransDs");
                });
#pragma warning restore 612, 618
        }
    }
}
