using Microsoft.EntityFrameworkCore.Migrations;

namespace Infastructure.Migrations
{
    public partial class mig3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ExchangeId",
                table: "History");

            migrationBuilder.DropColumn(
                name: "FromCurrencyCodeId",
                table: "History");

            migrationBuilder.DropColumn(
                name: "ToCurrencyCodeId",
                table: "History");

            migrationBuilder.AddColumn<int>(
                name: "ExchangeId1",
                table: "History",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "FromCurrencyCodeCurrencyId",
                table: "History",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ToCurrencyCodeCurrencyId",
                table: "History",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_History_ExchangeId1",
                table: "History",
                column: "ExchangeId1");

            migrationBuilder.CreateIndex(
                name: "IX_History_FromCurrencyCodeCurrencyId",
                table: "History",
                column: "FromCurrencyCodeCurrencyId");

            migrationBuilder.CreateIndex(
                name: "IX_History_ToCurrencyCodeCurrencyId",
                table: "History",
                column: "ToCurrencyCodeCurrencyId");

            migrationBuilder.AddForeignKey(
                name: "FK_History_Currency_FromCurrencyCodeCurrencyId",
                table: "History",
                column: "FromCurrencyCodeCurrencyId",
                principalTable: "Currency",
                principalColumn: "CurrencyId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_History_Currency_ToCurrencyCodeCurrencyId",
                table: "History",
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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_History_Currency_FromCurrencyCodeCurrencyId",
                table: "History");

            migrationBuilder.DropForeignKey(
                name: "FK_History_Currency_ToCurrencyCodeCurrencyId",
                table: "History");

            migrationBuilder.DropForeignKey(
                name: "FK_History_ExchangeRates_ExchangeId1",
                table: "History");

            migrationBuilder.DropIndex(
                name: "IX_History_ExchangeId1",
                table: "History");

            migrationBuilder.DropIndex(
                name: "IX_History_FromCurrencyCodeCurrencyId",
                table: "History");

            migrationBuilder.DropIndex(
                name: "IX_History_ToCurrencyCodeCurrencyId",
                table: "History");

            migrationBuilder.DropColumn(
                name: "ExchangeId1",
                table: "History");

            migrationBuilder.DropColumn(
                name: "FromCurrencyCodeCurrencyId",
                table: "History");

            migrationBuilder.DropColumn(
                name: "ToCurrencyCodeCurrencyId",
                table: "History");

            migrationBuilder.AddColumn<int>(
                name: "ExchangeId",
                table: "History",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "FromCurrencyCodeId",
                table: "History",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ToCurrencyCodeId",
                table: "History",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
