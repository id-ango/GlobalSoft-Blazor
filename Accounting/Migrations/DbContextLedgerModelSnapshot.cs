// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using eSoft.Ledger.Data;

namespace Accounting.Migrations
{
    [DbContext(typeof(DbContextLedger))]
    partial class DbContextLedgerModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.6")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("eSoft.Ledger.Model.GlAccount", b =>
                {
                    b.Property<int>("GlAccountId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("GlAcct")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("GlDept")
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("GlFisc1")
                        .HasColumnType("decimal(18,4)");

                    b.Property<decimal>("GlFisc10")
                        .HasColumnType("decimal(18,4)");

                    b.Property<decimal>("GlFisc11")
                        .HasColumnType("decimal(18,4)");

                    b.Property<decimal>("GlFisc12")
                        .HasColumnType("decimal(18,4)");

                    b.Property<decimal>("GlFisc2")
                        .HasColumnType("decimal(18,4)");

                    b.Property<decimal>("GlFisc3")
                        .HasColumnType("decimal(18,4)");

                    b.Property<decimal>("GlFisc4")
                        .HasColumnType("decimal(18,4)");

                    b.Property<decimal>("GlFisc5")
                        .HasColumnType("decimal(18,4)");

                    b.Property<decimal>("GlFisc6")
                        .HasColumnType("decimal(18,4)");

                    b.Property<decimal>("GlFisc7")
                        .HasColumnType("decimal(18,4)");

                    b.Property<decimal>("GlFisc8")
                        .HasColumnType("decimal(18,4)");

                    b.Property<decimal>("GlFisc9")
                        .HasColumnType("decimal(18,4)");

                    b.Property<string>("GlKurs")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("GlNama")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("GlPost")
                        .HasColumnType("datetime2");

                    b.Property<decimal>("GlPreFisc1")
                        .HasColumnType("decimal(18,4)");

                    b.Property<decimal>("GlPreFisc10")
                        .HasColumnType("decimal(18,4)");

                    b.Property<decimal>("GlPreFisc11")
                        .HasColumnType("decimal(18,4)");

                    b.Property<decimal>("GlPreFisc12")
                        .HasColumnType("decimal(18,4)");

                    b.Property<decimal>("GlPreFisc2")
                        .HasColumnType("decimal(18,4)");

                    b.Property<decimal>("GlPreFisc3")
                        .HasColumnType("decimal(18,4)");

                    b.Property<decimal>("GlPreFisc4")
                        .HasColumnType("decimal(18,4)");

                    b.Property<decimal>("GlPreFisc5")
                        .HasColumnType("decimal(18,4)");

                    b.Property<decimal>("GlPreFisc6")
                        .HasColumnType("decimal(18,4)");

                    b.Property<decimal>("GlPreFisc7")
                        .HasColumnType("decimal(18,4)");

                    b.Property<decimal>("GlPreFisc8")
                        .HasColumnType("decimal(18,4)");

                    b.Property<decimal>("GlPreFisc9")
                        .HasColumnType("decimal(18,4)");

                    b.Property<decimal>("GlSaldo")
                        .HasColumnType("decimal(18,4)");

                    b.Property<decimal>("GlSldAwal")
                        .HasColumnType("decimal(18,4)");

                    b.Property<string>("GlStatus")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("GlTipe")
                        .HasColumnType("int");

                    b.Property<string>("NamaLengkap")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("TipeGl")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("GlAccountId");

                    b.ToTable("GlAccounts");
                });

            modelBuilder.Entity("eSoft.Ledger.Model.GlCode", b =>
                {
                    b.Property<int>("GlCodeId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("KodeGl")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("NamaGl")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("GlCodeId");

                    b.ToTable("GlCodes");
                });

            modelBuilder.Entity("eSoft.Ledger.Model.GlTransD", b =>
                {
                    b.Property<int>("GlTransDId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<decimal>("Debet")
                        .HasColumnType("decimal(18,4)");

                    b.Property<string>("GlAcct")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("GlDept")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("GlTransHId")
                        .HasColumnType("int");

                    b.Property<decimal>("JumKurs")
                        .HasColumnType("decimal(18,4)");

                    b.Property<decimal>("Jumlah")
                        .HasColumnType("decimal(18,4)");

                    b.Property<string>("Keterangan")
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("Kredit")
                        .HasColumnType("decimal(18,4)");

                    b.Property<string>("Kurs")
                        .HasMaxLength(3)
                        .HasColumnType("nvarchar(3)");

                    b.Property<decimal>("NomKurs")
                        .HasColumnType("decimal(18,4)");

                    b.Property<bool>("NonPPn")
                        .HasColumnType("bit");

                    b.HasKey("GlTransDId");

                    b.HasIndex("GlTransHId");

                    b.ToTable("GlTransDs");
                });

            modelBuilder.Entity("eSoft.Ledger.Model.GlTransH", b =>
                {
                    b.Property<int>("GlTransHId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<decimal>("Debet")
                        .HasColumnType("decimal(18,4)");

                    b.Property<string>("DocNo")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("GlMemo")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("KodeGl")
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("Kredit")
                        .HasColumnType("decimal(18,4)");

                    b.Property<string>("Kurs")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("NonPPn")
                        .HasColumnType("bit");

                    b.Property<decimal>("Saldo")
                        .HasColumnType("decimal(18,4)");

                    b.Property<DateTime>("Tanggal")
                        .HasColumnType("datetime2");

                    b.HasKey("GlTransHId");

                    b.ToTable("GlTransHs");
                });

            modelBuilder.Entity("eSoft.Ledger.Model.GlTransD", b =>
                {
                    b.HasOne("eSoft.Ledger.Model.GlTransH", "GlTransH")
                        .WithMany("GlTransDs")
                        .HasForeignKey("GlTransHId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("GlTransH");
                });

            modelBuilder.Entity("eSoft.Ledger.Model.GlTransH", b =>
                {
                    b.Navigation("GlTransDs");
                });
#pragma warning restore 612, 618
        }
    }
}
