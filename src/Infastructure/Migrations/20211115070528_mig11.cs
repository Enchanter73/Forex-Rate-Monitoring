using Microsoft.EntityFrameworkCore.Migrations;

namespace Infastructure.Migrations
{
    public partial class mig11 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BaseCurreny",
                table: "ExchangeRates");

            migrationBuilder.DropColumn(
                name: "QuoteCurrency",
                table: "ExchangeRates");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "BaseCurreny",
                table: "ExchangeRates",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "QuoteCurrency",
                table: "ExchangeRates",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
