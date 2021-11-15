﻿// <auto-generated />
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
    [Migration("20211115045616_mi7")]
    partial class mi7
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

                    b.Property<int?>("FromCurrencyCurrencyId")
                        .HasColumnType("int");

                    b.Property<int?>("ToCurrencyCurrencyId")
                        .HasColumnType("int");

                    b.HasKey("ExchangeId");

                    b.HasIndex("FromCurrencyCurrencyId");

                    b.HasIndex("ToCurrencyCurrencyId");

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

                    b.Property<string>("ExchangeRate")
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("ExchangeRate");

                    b.Property<int?>("ExchangeRateModelExchangeId")
                        .HasColumnType("int");

                    b.Property<int?>("FromCurrencyCodeCurrencyId")
                        .HasColumnType("int");

                    b.Property<int?>("ToCurrencyCodeCurrencyId")
                        .HasColumnType("int");

                    b.HasKey("HistoryId");

                    b.HasIndex("ExchangeRateModelExchangeId");

                    b.HasIndex("FromCurrencyCodeCurrencyId");

                    b.HasIndex("ToCurrencyCodeCurrencyId");

                    b.ToTable("History");
                });

            modelBuilder.Entity("ApplicationCore.Models.ExchangeRateModel", b =>
                {
                    b.HasOne("ApplicationCore.Models.Currency", "FromCurrency")
                        .WithMany("FromExchangeRateModels")
                        .HasForeignKey("FromCurrencyCurrencyId");

                    b.HasOne("ApplicationCore.Models.Currency", "ToCurrency")
                        .WithMany("ToExchangeRateModels")
                        .HasForeignKey("ToCurrencyCurrencyId");

                    b.Navigation("FromCurrency");

                    b.Navigation("ToCurrency");
                });

            modelBuilder.Entity("ApplicationCore.Models.History", b =>
                {
                    b.HasOne("ApplicationCore.Models.ExchangeRateModel", "ExchangeRateModel")
                        .WithMany("Histories")
                        .HasForeignKey("ExchangeRateModelExchangeId");

                    b.HasOne("ApplicationCore.Models.Currency", "FromCurrencyCode")
                        .WithMany()
                        .HasForeignKey("FromCurrencyCodeCurrencyId");

                    b.HasOne("ApplicationCore.Models.Currency", "ToCurrencyCode")
                        .WithMany()
                        .HasForeignKey("ToCurrencyCodeCurrencyId");

                    b.Navigation("ExchangeRateModel");

                    b.Navigation("FromCurrencyCode");

                    b.Navigation("ToCurrencyCode");
                });

            modelBuilder.Entity("ApplicationCore.Models.Currency", b =>
                {
                    b.Navigation("FromExchangeRateModels");

                    b.Navigation("ToExchangeRateModels");
                });

            modelBuilder.Entity("ApplicationCore.Models.ExchangeRateModel", b =>
                {
                    b.Navigation("Histories");
                });
#pragma warning restore 612, 618
        }
    }
}
