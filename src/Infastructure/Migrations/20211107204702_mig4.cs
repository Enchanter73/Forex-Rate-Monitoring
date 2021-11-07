using Microsoft.EntityFrameworkCore.Migrations;

namespace Infastructure.Migrations
{
    public partial class mig4 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ExchangeRates_Currency_FromCurrencyCodeCurrencyId",
                table: "ExchangeRates");

            migrationBuilder.DropForeignKey(
                name: "FK_ExchangeRates_Currency_ToCurrencyCodeCurrencyId",
                table: "ExchangeRates");

            migrationBuilder.DropForeignKey(
                name: "FK_History_ExchangeRates_ExchangeId1",
                table: "History");

            migrationBuilder.RenameColumn(
                name: "ExchangeId1",
                table: "History",
                newName: "ExchangeRateModelExchangeId");

            migrationBuilder.RenameIndex(
                name: "IX_History_ExchangeId1",
                table: "History",
                newName: "IX_History_ExchangeRateModelExchangeId");

            migrationBuilder.RenameColumn(
                name: "ToCurrencyCodeCurrencyId",
                table: "ExchangeRates",
                newName: "ToCurrencyCurrencyId");

            migrationBuilder.RenameColumn(
                name: "FromCurrencyCodeCurrencyId",
                table: "ExchangeRates",
                newName: "FromCurrencyCurrencyId");

            migrationBuilder.RenameIndex(
                name: "IX_ExchangeRates_ToCurrencyCodeCurrencyId",
                table: "ExchangeRates",
                newName: "IX_ExchangeRates_ToCurrencyCurrencyId");

            migrationBuilder.RenameIndex(
                name: "IX_ExchangeRates_FromCurrencyCodeCurrencyId",
                table: "ExchangeRates",
                newName: "IX_ExchangeRates_FromCurrencyCurrencyId");

            migrationBuilder.AddColumn<int>(
                name: "ExchangeId",
                table: "History",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "FromCurrencyCodeId",
                table: "ExchangeRates",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ToCurrencyCodeId",
                table: "ExchangeRates",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddForeignKey(
                name: "FK_ExchangeRates_Currency_FromCurrencyCurrencyId",
                table: "ExchangeRates",
                column: "FromCurrencyCurrencyId",
                principalTable: "Currency",
                principalColumn: "CurrencyId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ExchangeRates_Currency_ToCurrencyCurrencyId",
                table: "ExchangeRates",
                column: "ToCurrencyCurrencyId",
                principalTable: "Currency",
                principalColumn: "CurrencyId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_History_ExchangeRates_ExchangeRateModelExchangeId",
                table: "History",
                column: "ExchangeRateModelExchangeId",
                principalTable: "ExchangeRates",
                principalColumn: "ExchangeId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ExchangeRates_Currency_FromCurrencyCurrencyId",
                table: "ExchangeRates");

            migrationBuilder.DropForeignKey(
                name: "FK_ExchangeRates_Currency_ToCurrencyCurrencyId",
                table: "ExchangeRates");

            migrationBuilder.DropForeignKey(
                name: "FK_History_ExchangeRates_ExchangeRateModelExchangeId",
                table: "History");

            migrationBuilder.DropColumn(
                name: "ExchangeId",
                table: "History");

            migrationBuilder.DropColumn(
                name: "FromCurrencyCodeId",
                table: "ExchangeRates");

            migrationBuilder.DropColumn(
                name: "ToCurrencyCodeId",
                table: "ExchangeRates");

            migrationBuilder.RenameColumn(
                name: "ExchangeRateModelExchangeId",
                table: "History",
                newName: "ExchangeId1");

            migrationBuilder.RenameIndex(
                name: "IX_History_ExchangeRateModelExchangeId",
                table: "History",
                newName: "IX_History_ExchangeId1");

            migrationBuilder.RenameColumn(
                name: "ToCurrencyCurrencyId",
                table: "ExchangeRates",
                newName: "ToCurrencyCodeCurrencyId");

            migrationBuilder.RenameColumn(
                name: "FromCurrencyCurrencyId",
                table: "ExchangeRates",
                newName: "FromCurrencyCodeCurrencyId");

            migrationBuilder.RenameIndex(
                name: "IX_ExchangeRates_ToCurrencyCurrencyId",
                table: "ExchangeRates",
                newName: "IX_ExchangeRates_ToCurrencyCodeCurrencyId");

            migrationBuilder.RenameIndex(
                name: "IX_ExchangeRates_FromCurrencyCurrencyId",
                table: "ExchangeRates",
                newName: "IX_ExchangeRates_FromCurrencyCodeCurrencyId");

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

            migrationBuilder.AddForeignKey(
                name: "FK_History_ExchangeRates_ExchangeId1",
                table: "History",
                column: "ExchangeId1",
                principalTable: "ExchangeRates",
                principalColumn: "ExchangeId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
