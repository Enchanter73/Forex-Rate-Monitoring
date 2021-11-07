using Microsoft.EntityFrameworkCore.Migrations;

namespace Infastructure.Migrations
{
    public partial class mig2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FromCurrencyCodeId",
                table: "ExchangeRates");

            migrationBuilder.DropColumn(
                name: "ToCurrencyCodeId",
                table: "ExchangeRates");

            migrationBuilder.AddColumn<int>(
                name: "FromCurrencyCodeCurrencyId",
                table: "ExchangeRates",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ToCurrencyCodeCurrencyId",
                table: "ExchangeRates",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ExchangeRates_FromCurrencyCodeCurrencyId",
                table: "ExchangeRates",
                column: "FromCurrencyCodeCurrencyId");

            migrationBuilder.CreateIndex(
                name: "IX_ExchangeRates_ToCurrencyCodeCurrencyId",
                table: "ExchangeRates",
                column: "ToCurrencyCodeCurrencyId");

            migrationBuilder.AddForeignKey(
                name: "FK_ExchangeRates_Currency_FromCurrencyCodeCurrencyId",
                table: "ExchangeRates",
                column: "FromCurrencyCodeCurrencyId",
                principalTable: "Currency",
                principalColumn: "CurrencyId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ExchangeRates_Currency_ToCurrencyCodeCurrencyId",
                table: "ExchangeRates",
                column: "ToCurrencyCodeCurrencyId",
                principalTable: "Currency",
                principalColumn: "CurrencyId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ExchangeRates_Currency_FromCurrencyCodeCurrencyId",
                table: "ExchangeRates");

            migrationBuilder.DropForeignKey(
                name: "FK_ExchangeRates_Currency_ToCurrencyCodeCurrencyId",
                table: "ExchangeRates");

            migrationBuilder.DropIndex(
                name: "IX_ExchangeRates_FromCurrencyCodeCurrencyId",
                table: "ExchangeRates");

            migrationBuilder.DropIndex(
                name: "IX_ExchangeRates_ToCurrencyCodeCurrencyId",
                table: "ExchangeRates");

            migrationBuilder.DropColumn(
                name: "FromCurrencyCodeCurrencyId",
                table: "ExchangeRates");

            migrationBuilder.DropColumn(
                name: "ToCurrencyCodeCurrencyId",
                table: "ExchangeRates");

            migrationBuilder.AddColumn<string>(
                name: "FromCurrencyCodeId",
                table: "ExchangeRates",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ToCurrencyCodeId",
                table: "ExchangeRates",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
