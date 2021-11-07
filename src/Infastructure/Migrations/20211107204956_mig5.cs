using Microsoft.EntityFrameworkCore.Migrations;

namespace Infastructure.Migrations
{
    public partial class mig5 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ExchangeId",
                table: "History");

            migrationBuilder.DropColumn(
                name: "FromCurrencyCodeId",
                table: "ExchangeRates");

            migrationBuilder.DropColumn(
                name: "ToCurrencyCodeId",
                table: "ExchangeRates");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
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
        }
    }
}
