using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Infastructure.Migrations
{
    public partial class mig14 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "History");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "History",
                columns: table => new
                {
                    HistoryId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ExchangeRate = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ExchangeRateModelExchangeId = table.Column<int>(type: "int", nullable: true),
                    FromCurrencyCodeCurrencyId = table.Column<int>(type: "int", nullable: true),
                    ToCurrencyCodeCurrencyId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_History", x => x.HistoryId);
                    table.ForeignKey(
                        name: "FK_History_Currency_FromCurrencyCodeCurrencyId",
                        column: x => x.FromCurrencyCodeCurrencyId,
                        principalTable: "Currency",
                        principalColumn: "CurrencyId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_History_Currency_ToCurrencyCodeCurrencyId",
                        column: x => x.ToCurrencyCodeCurrencyId,
                        principalTable: "Currency",
                        principalColumn: "CurrencyId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_History_ExchangeRates_ExchangeRateModelExchangeId",
                        column: x => x.ExchangeRateModelExchangeId,
                        principalTable: "ExchangeRates",
                        principalColumn: "ExchangeId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_History_ExchangeRateModelExchangeId",
                table: "History",
                column: "ExchangeRateModelExchangeId");

            migrationBuilder.CreateIndex(
                name: "IX_History_FromCurrencyCodeCurrencyId",
                table: "History",
                column: "FromCurrencyCodeCurrencyId");

            migrationBuilder.CreateIndex(
                name: "IX_History_ToCurrencyCodeCurrencyId",
                table: "History",
                column: "ToCurrencyCodeCurrencyId");
        }
    }
}
