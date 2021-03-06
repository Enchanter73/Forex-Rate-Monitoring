// <auto-generated />
using System;
using Infastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Infastructure.Migrations
{
    [DbContext(typeof(FER_Context))]
    [Migration("20211107181214_mig2")]
    partial class mig2
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.11")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("ApplicationCore.Models.Currency", b =>
                {
                    b.Property<int>("CurrencyId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("CurrencyName")
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("CurrencyName");

                    b.HasKey("CurrencyId");

                    b.ToTable("Currency");
                });

            modelBuilder.Entity("ApplicationCore.Models.ExchangeRateModel", b =>
                {
                    b.Property<int>("ExchangeId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime2")
                        .HasColumnName("Date");

                    b.Property<string>("ExchangeRate")
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("ExchangeRate");

                    b.Property<int?>("FromCurrencyCodeCurrencyId")
                        .HasColumnType("int");

                    b.Property<int?>("ToCurrencyCodeCurrencyId")
                        .HasColumnType("int");

                    b.HasKey("ExchangeId");

                    b.HasIndex("FromCurrencyCodeCurrencyId");

                    b.HasIndex("ToCurrencyCodeCurrencyId");

                    b.ToTable("ExchangeRates");
                });

            modelBuilder.Entity("ApplicationCore.Models.History", b =>
                {
                    b.Property<int>("HistoryId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime2")
                        .HasColumnName("Date");

                    b.Property<int>("ExchangeId")
                        .HasColumnType("int")
                        .HasColumnName("ExchangeId");

                    b.Property<string>("ExchangeRate")
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("ExchangeRate");

                    b.Property<string>("FromCurrencyCode")
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("FromCurrencyCodeId");

                    b.Property<string>("ToCurrencyCode")
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("ToCurrencyCodeId");

                    b.HasKey("HistoryId");

                    b.ToTable("History");
                });

            modelBuilder.Entity("ApplicationCore.Models.ExchangeRateModel", b =>
                {
                    b.HasOne("ApplicationCore.Models.Currency", "FromCurrencyCode")
                        .WithMany()
                        .HasForeignKey("FromCurrencyCodeCurrencyId");

                    b.HasOne("ApplicationCore.Models.Currency", "ToCurrencyCode")
                        .WithMany()
                        .HasForeignKey("ToCurrencyCodeCurrencyId");

                    b.Navigation("FromCurrencyCode");

                    b.Navigation("ToCurrencyCode");
                });
#pragma warning restore 612, 618
        }
    }
}
